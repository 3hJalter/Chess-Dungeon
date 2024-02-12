using System;
using HoangHH.Data.CharacterConfigData;
using HoangHH.InGame.Grid;
using HoangHH.Manager;
using Lean.Pool;
using UnityEngine;

namespace HoangHH.InGame.Character.Base
{
    public abstract class BCharacter : HMonoBehaviour, ICharacter, IPoolable
    {
        [SerializeField] protected CharacterConfigData configData;
        private GameGridCell _currentCell;
        private bool _isTouched;
        private Action _onUnTouch;
        
        // Remove this method later
        private void OnMouseDown()
        {
            if (!_isTouched)
                OnTouchCharacter();
            else
                OnUnTouchCharacter();
        }

        private void OnMouseUp()
        {
            HLog.Log(DevID.Hoang, "OnMouseUp");
        }

        public virtual void OnSpawn()
        {
            // Get cell from spawn position
            GameGridCell initCell = InGameManager.Instance.GameGrid.GetGridCell(Tf.position);
            OnInit(initCell);
        }

        public virtual void OnDespawn()
        {
            OnExitCell();
            _onUnTouch -= OnUnTouchCharacter;
        }
        
        public virtual void OnInit(GameGridCell cellInit)
        {
            OnEnterCell(cellInit);
            _onUnTouch += OnUnTouchCharacter;
        }

        public void OnEnterCell(GameGridCell cell)
        {
            _currentCell = cell;
            _currentCell.AttachCharacter(this);
        }

        public void OnExitCell()
        {
            _currentCell.DetachCharacter();
        }

        protected virtual void OnTouchCharacter()
        {
            _isTouched = true;
            EventGlobalManager.Instance.OnCharacterUnTouch?.Dispatch();
            EventGlobalManager.Instance.OnCharacterUnTouch?.AddListener(_onUnTouch);
            // TODO: Logic when character is clicked
            // 1. Show character's info
            // 2. Show character's move range by highlight the cell around it (more highlight attack range if it's an enemy)
            // 3. Enable player to move the character to another cell by clicking on the cell
            // 4. Enable player to attack enemy by clicking on the enemy character (if enemy is in the move range)
            // 5. Enable player to use skill by clicking on the skill button and then click on the target cell or character
            HLog.Log(DevID.Hoang, $"Character {gameObject.name} OnTouch");
        }
        
        protected virtual void OnUnTouchCharacter() 
        {
            _isTouched = false;
            EventGlobalManager.Instance.OnCharacterUnTouch?.RemoveListener(_onUnTouch);
            // TODO: Logic when character is un-clicked
            // 1. Hide character's info
            // 2. Hide character's move range by un-highlight the cell around it (more un-highlight attack range if it's an enemy)
            // 3. Disable player to move the character to another cell by clicking on the cell
            // 4. Disable player to attack enemy by clicking on the enemy character (if enemy is in the move range)
            // 5. Disable player to use skill by clicking on the skill button and then click on the target cell or character
            HLog.Log(DevID.Hoang, $"Character {gameObject.name} OnUnTouch");
        }
    }
}
