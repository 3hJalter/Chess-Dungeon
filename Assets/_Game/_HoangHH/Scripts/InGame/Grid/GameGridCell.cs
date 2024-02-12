using System;
using HoangHH.DesignPattern.Memento;
using HoangHH.InGame.Character.Base;
using HoangHH.Utilities.Grid;

namespace HoangHH.InGame.Grid
{
    public class GameGridCell : GridCell<GameGridCellData>, IOriginator
    {
        public event Action<int, int, bool> OnValueChange;
        
        public GameGridCell()
        {
            data = new GameGridCellData();
        }

        #region Handling Data

        public BCharacter CharacterOnCell
        {
            get => data.characterOn;
            private set => data.characterOn = value;
        }
        
        public void DetachCharacter()
        {
            CharacterOnCell = null;
            ValueChange();
        }
        
        public void AttachCharacter(BCharacter character)
        {
            CharacterOnCell = character;
            HLog.Log(DevID.Hoang, $"Add Character {character.name} to Cell {X}x{Y}");
            ValueChange();
        }
        
        public int DmgDealOnCell => data.dmgDealOnCell;
        
        public virtual void ValueChange(bool isRevert = false)
        {
            OnValueChange?.Invoke(X, Y, isRevert);
        }


        #endregion

        #region Handling when be highlighted/un-highlighted

        public void OnHighlighted()
        {
            HLog.Log(DevID.Hoang, $"OnPointerEnter at cell {X} - {Y}");
            // TODO: Highlight cell
            // 1. Highlight cell
            // 1.1 Store the cell that it's being highlighted at InGameManager
            // 2. Raise event to the cell that it's being highlighted 
            // 2.1 Show Damage deal on cell
            // 2.2 Show Character on cell, its stats, and its move (attack) range
        }
        
        public void OnUnHighlighted()
        {
            HLog.Log(DevID.Hoang, $"OnPointerExit at cell {X} - {Y}");
            // TODO: Un-highlight cell
            // 1. Un-highlight cell
            // 1.1 Remove the cell that it's being highlighted at InGameManager (Set it to null)
            // 2. Raise event to the cell that it's being un-highlighted
            // 2.1 Hide Damage deal on cell
            // 2.2 Hide Character on cell, its stats, and its move (attack) range
        }

        #endregion

        #region SAVING DATA
        public IMemento Save()
        {
            return new CellMemento(this);
        }
        #endregion
    }
    
    public class GameGridCellData : IReset
    {
        public BCharacter characterOn;
        public int dmgDealOnCell;

        public void ResetData()
        {
            characterOn = null;
            dmgDealOnCell = 0;
        }
    }
    
    public readonly struct CellMemento : IMemento
    {
        private readonly GameGridCell main;
        public int Id => main.GetHashCode();
        public CellMemento(GameGridCell main)
        {
            this.main = main;
        }

        public void Restore()
        {
            main.ValueChange(true);
        }
    }
}
