using System;
using System.Collections.Generic;
using System.Linq;
using HoangHH.DesignPattern.Memento;
using HoangHH.Utilities.Grid;
using UnityEngine;

namespace HoangHH.InGame.Grid
{
    public class GameGrid : Grid<GameGridCell, GameGridCellData>, IOriginator
    {
        private readonly List<IMemento> cellMementos = new();
        private readonly List<Vector2Int> cellPos = new();

        public GameGrid(int width, int height, float cellSize, Vector3 originPosition = default,
            Func<GridCell<GameGridCellData>> constructorCell = null, GridPlane gridPlaneType = GridPlane.XY) : base(
            width, height, cellSize, originPosition, constructorCell, gridPlaneType)
        {
            for (int x = 0; x < GridArray.GetLength(0); x++)
            for (int y = 0; y < GridArray.GetLength(1); y++)
            {
                GridArray[x, y].OnValueChange += OnGridCellValueChange;
            }
        }

        //Grid is change or not when compare to last save state
        public bool IsChange { get; private set; }

        public override void SetGridCell(int x, int y, GameGridCell value)
        {
            if (value is null) return;
            if (x < 0 || y < 0 || x >= Width || y >= Height) return;
            GridArray[x, y].OnValueChange -= OnGridCellValueChange;
            GridArray[x, y] = value;
            GridArray[x, y].OnValueChange += OnGridCellValueChange;
        }

        protected override void OnGridCellValueChange(int x, int y, bool isRevert)
        {
            base.OnGridCellValueChange(x, y, isRevert);
            if (isRevert) return;
            if (!cellPos.Any(pos => pos.x == x && pos.y == y)) cellMementos.Add(GridArray[x, y].Save());
        }

        public void Reset()
        {
            IsChange = false;
            cellMementos.Clear();
            cellPos.Clear();
            foreach (GameGridCell cell in GridArray) cell.Data.ResetData();
        }

        #region Saving Data

        public void CompleteObjectInit()
        {
            cellMementos.Clear();
            IsChange = false;
        }

        public IMemento Save()
        {
            GridMemento save = new(this, cellMementos);
            cellMementos.Clear();
            cellPos.Clear();
            IsChange = false;
            return save;
        }

        public struct GridMemento : IMemento
        {
            private GameGrid main;
            private readonly List<CellMemento> cellMementos;
            public int Id => 0;

            public GridMemento(GameGrid main, params object[] data)
            {
                this.main = main;
                cellMementos = new List<CellMemento>();

                foreach (IMemento memento in (List<IMemento>)data[0]) cellMementos.Add((CellMemento)memento);

            }

            public void Restore()
            {
                foreach (CellMemento memento in cellMementos) memento.Restore();
            }

            public void Merge(GridMemento memento)
            {
                for (int index = 0; index < memento.cellMementos.Count; index++)
                {
                    CellMemento cellMemento = memento.cellMementos[index];
                    if (cellMementos.All(x => x.Id != cellMemento.Id)) cellMementos.Add(cellMemento);
                }
            }
        }

        #endregion

        public bool IsValidCell(int i, int i1)
        {
            return i >= 0 && i < Width && i1 >= 0 && i1 < Height;
        }
    }
}
