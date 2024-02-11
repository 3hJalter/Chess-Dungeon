using HoangHH.Data.CharacterConfigData;
using Lean.Pool;
using UnityEngine;

namespace HoangHH.InGame.Character.Base
{
    public class BCharacter : HMonoBehaviour, IPoolable
    {
        [SerializeField] protected CharacterConfigData configData;


        public virtual void OnSpawn()
        {
        }

        public virtual void OnDespawn()
        {
        }
    }
}
