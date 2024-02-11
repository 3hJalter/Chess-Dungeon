using Sirenix.OdinInspector;
using UnityEngine;

namespace HoangHH.Data.CharacterConfigData
{
    [CreateAssetMenu(fileName = "CharacterConfigData", menuName = "DataSO/CharacterConfig", order = 1)]
    public class CharacterConfigData : SerializedScriptableObject
    {
        public int health;
    }
}
