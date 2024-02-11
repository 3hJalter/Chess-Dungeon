using HoangHH.Data.PrefabData;
using HoangHH.DesignPattern;
using UnityEngine;

namespace HoangHH.Manager
{
    public enum DataType
    {
        Board,
        Piece,
        Player
    }

    public class DataManager : Singleton<DataManager>
    {
        #region Prefab Container
        [SerializeField] private Transform poolPrefabContainer;
        private Transform PoolPrefabContainer => poolPrefabContainer
            ? poolPrefabContainer
            : poolPrefabContainer = new GameObject("___ Pool Container ___").transform;
        #endregion
        
        [SerializeField] private BoardPrefabData boardPrefabData;
        [SerializeField] private CharacterPrefabData characterPrefabData;

        

        public T GetData<T, TD>(DataType type, TD objectType)
        {
            return type switch
            {
                DataType.Board => (T)(object)boardPrefabData.GetBoard((BoardType)(object)objectType),
                DataType.Player => (T)characterPrefabData.GetPlayer((PlayerType)(object)objectType),
                _ => default
            };
        }

        public T SpawnData<T, TD>(DataType type, TD objectType, Vector3 position = default,
            Quaternion rotation = default, Transform parent = null)
        {
            return type switch
            {
                DataType.Board => (T)(object)boardPrefabData.SpawnBoard((BoardType)(object)objectType, position,
                    rotation, parent ? parent : PoolPrefabContainer),
                DataType.Player => (T)characterPrefabData.SpawnPlayer((PlayerType)(object)objectType, position,
                    rotation, parent ? parent : PoolPrefabContainer),
                _ => default
            };
        }
    }
}
