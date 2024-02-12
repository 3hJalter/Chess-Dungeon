using HoangHH.InGame.Grid;
using HoangHH.Manager;
using Lean.Pool;
using UnityEngine.EventSystems;

namespace HoangHH.InGame.Board
{
    public class BBoard : HMonoBehaviour, IPoolable, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        private GameGridCell _cellInit;
        
        public void OnSpawn()
        {
            // Get cell from spawn position
            GameGridCell initCell = InGameManager.Instance.GameGrid.GetGridCell(Tf.position);
            OnInit(initCell);
        }

        public void OnDespawn()
        {
            _cellInit = null;
        }
        
        private void OnInit(GameGridCell cellInit)
        {
            _cellInit = cellInit;
        }
        
        
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _cellInit.OnHighlighted();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _cellInit.OnUnHighlighted();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            // TODO: Pointer Down at cell
            // Storing the first clicked cell as _cellInit at InGameManager
            // Start a Timer to check if it's a long press
            HLog.Log(DevID.Hoang, $"OnPointerDown at cell {_cellInit.X} - {_cellInit.Y}");
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            // TODO: Pointer Up at cell
            // Stop the Timer
            // If it's a long press
            // Check if the last enter cell is the same as the cell when the long press started
            // If yes, Check if the cell is in the move range of the Player, if it's in the move range, move the Player to that cell
            // If no, do nothing
            // Set the first clicked cell to null
            HLog.Log(DevID.Hoang, $"OnPointerUp at cell {_cellInit.X} - {_cellInit.Y}");
        }
    }
}
