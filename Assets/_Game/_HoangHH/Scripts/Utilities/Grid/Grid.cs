using System;
using System.Collections.Generic;
using UnityEngine;

namespace HoangHH.Utilities.Grid
{
    public class Grid<T, TD> where T : GridCell<TD>
    {
        private readonly List<Vector2Int> cellPos = new();

        protected Grid(int width, int height, float cellSize, Vector3 originPosition = default,
            Func<GridCell<TD>> constructorCell = null, GridPlane gridPlaneType = GridPlane.XY)
        {
            Width = width;
            Height = height;
            CellSize = cellSize;
            OriginPosition = originPosition;
            GridPlaneType = gridPlaneType;

            GridArray = new T[width, height];

            for (int x = 0; x < GridArray.GetLength(0); x++)
            for (int y = 0; y < GridArray.GetLength(1); y++)
            {
                if (constructorCell != null) GridArray[x, y] = (T)constructorCell();
                GridArray[x, y].SetCellPosition(x, y);
                GridArray[x, y].Size = cellSize;
                GridArray[x, y].GridPlaneType = gridPlaneType;
                switch (gridPlaneType)
                {
                    case GridPlane.XY:
                        GridArray[x, y].UpdateWorldPosition(originPosition.x, originPosition.y);
                        break;
                    case GridPlane.YZ:
                        GridArray[x, y].UpdateWorldPosition(originPosition.y, originPosition.z);
                        break;
                    case GridPlane.XZ:
                    default:
                        GridArray[x, y].UpdateWorldPosition(originPosition.x, originPosition.z);
                        break;
                }
            }
        }

        public float CellSize { get; protected set; }

        public int Width { get; protected set; }

        public int Height { get; protected set; }

        public Vector3 OriginPosition { get; protected set; }

        public GridPlane GridPlaneType { get; protected set; }

        public T[,] GridArray { get; protected set; }

        public Vector3 GetWorldPosition(int x, int y)
        {
            return GetUnitVector3(x, y) * CellSize + OriginPosition;
        }


        public Vector3 GetWorldPosition(Vector3 worldPosition)
        {
            (int x, int y) = GetGridPosition(worldPosition);

            return GetUnitVector3(x, y) * CellSize + OriginPosition;
        }

        public (int, int) GetGridPosition(Vector3 worldPosition)
        {
            Vector3 realPos = worldPosition - OriginPosition;
            return GridPlaneType switch
            {
                GridPlane.XY => (Mathf.FloorToInt(realPos.x / CellSize), Mathf.FloorToInt(realPos.y / CellSize)),
                GridPlane.XZ => (Mathf.FloorToInt(realPos.x / CellSize), Mathf.FloorToInt(realPos.z / CellSize)),
                GridPlane.YZ => (Mathf.FloorToInt(realPos.y / CellSize), Mathf.FloorToInt(realPos.z / CellSize)),
                _ => default
            };
        }

        public virtual void SetGridCell(int x, int y, T value)
        {
            if (value is null) return;
            if (x >= 0 && y >= 0 && x < Width && y < Height) GridArray[x, y] = value;
        }

        public void SetGridCell(Vector3 position, T value)
        {
            int x, y;
            (x, y) = GetGridPosition(position);
            SetGridCell(x, y, value);
        }

        public T GetGridCell(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < Width && y < Height) return GridArray[x, y];
            return default;
        }

        public T GetGridCell(Vector3 worldPosition)
        {
            int x, y;
            (x, y) = GetGridPosition(worldPosition);
            if (x >= 0 && y >= 0 && x < Width && y < Height) return GridArray[x, y];
            return default;
        }

        protected virtual void OnGridCellValueChange(int x, int y, bool isRevert)
        {
            if (isRevert) return;
            //NOTE: TEST
            cellPos.Add(new Vector2Int(x, y));
        }
        
        private Vector3 GetUnitVector3(float val1, float val2)
        {
            switch (GridPlaneType)
            {
                case GridPlane.XY:
                    return new Vector3(val1, val2, 0);
                case GridPlane.XZ:
                    return new Vector3(val1, 0, val2);
                case GridPlane.YZ:
                    return new Vector3(0, val1, val2);
            }

            return default;
        }

        #region VISIT CLASS

        public abstract class PathfindingAlgorithm
        {
            protected Grid<T, TD> grid;
            public abstract List<T> FindPath(int startX, int startY, int endX, int endY, Grid<T, TD> grid);
        }

        #endregion
    }
}
