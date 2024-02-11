using System.Collections.Generic;
using HoangHH.InGame.Board;
using Lean.Pool;
using Sirenix.OdinInspector;
using UnityEngine;

namespace HoangHH.Data.PrefabData
{
    public enum BoardType
    {
        NormalBlack,
        NormalWhite,
    }
    [CreateAssetMenu(fileName = "BoardPrefabData", menuName = "DataSO/PrefabData/Board", order = 1)]
    public class BoardPrefabData : SerializedScriptableObject
    {
        // ReSharper disable once CollectionNeverUpdated.Local
        // ReSharper disable once Unity.RedundantSerializeFieldAttribute
        [SerializeField] private readonly Dictionary<BoardType, BBoard> _boards;
        
        public BBoard GetBoard(BoardType type)
        {
            return _boards[type];
        }
        
        public BBoard SpawnBoard(BoardType type, Vector3 position, Quaternion rotation, Transform parent)
        {
            // Try getting the board from dictionary
            if (_boards.TryGetValue(type, out BBoard board)) return LeanPool.Spawn(board, position, rotation, parent);
            HLog.Log(DevID.Hoang, $"Board type {type} not found");
            return null;
        }
    }
}
