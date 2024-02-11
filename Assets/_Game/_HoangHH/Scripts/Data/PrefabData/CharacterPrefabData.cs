using System.Collections.Generic;
using HoangHH.InGame.Character.Base;
using Lean.Pool;
using Sirenix.OdinInspector;
using UnityEngine;

namespace HoangHH.Data.PrefabData
{
    public enum PlayerType
    {
        Normal = 0,
        Emperor = 1,
        Shogun = 2,
    }
    
    public enum EnemyType {
        Normal = 0,
    }
    
    [CreateAssetMenu(fileName = "CharacterPrefabData", menuName = "DataSO/PrefabData/Character", order = 1)]
    public class CharacterPrefabData : SerializedScriptableObject
    {
        // ReSharper disable once CollectionNeverUpdated.Local
        // ReSharper disable once Unity.RedundantSerializeFieldAttribute
        [SerializeField] private Dictionary<PlayerType, IPlayer> _players;
        // ReSharper disable once CollectionNeverUpdated.Local
        // ReSharper disable once Unity.RedundantSerializeFieldAttribute
        [SerializeField] private Dictionary<EnemyType, IEnemy> _enemies;
        
        public IPlayer GetPlayer(PlayerType type)
        {
            return _players[type];
        }
        
        public IEnemy GetEnemy(EnemyType type)
        {
            return _enemies[type];
        }
        
        public IPlayer SpawnPlayer(PlayerType type, Vector3 position, Quaternion rotation, Transform parent)
        {
            // Try getting the player from dictionary
            if (_players.TryGetValue(type, out IPlayer player)) return (IPlayer) LeanPool.Spawn((BCharacter) player, position, rotation, parent);
            HLog.Log(DevID.Hoang, $"Player type {type} not found");
            return null;
        }
        
        public IEnemy SpawnEnemy(EnemyType type, Vector3 position, Quaternion rotation, Transform parent)
        {
            // Try getting the enemy from dictionary
            if (_enemies.TryGetValue(type, out IEnemy enemy)) return (IEnemy) LeanPool.Spawn((BCharacter) enemy, position, rotation, parent);
            HLog.Log(DevID.Hoang, $"Enemy type {type} not found");
            return null;
        }
    }
}
