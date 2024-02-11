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

        public BCharacter CharacterOnCell
        {
            get => data.characterOn;
            private set => data.characterOn = value;
        }
        
        public void AddCharacter(BCharacter character)
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
