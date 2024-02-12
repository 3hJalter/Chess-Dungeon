using HoangHH.InGame.Character.Base;
using UnityEngine.EventSystems;

namespace HoangHH.InGame.Character
{
    public class Player : BCharacter, IPlayer
    {
        public override void OnSpawn()
        {
            base.OnSpawn();
            HLog.Log(DevID.Hoang, "Player OnSpawn");
        }
        

        public override void OnDespawn()
        {
            base.OnDespawn();
        }
        
        public void Move()
        {
            HLog.Log(DevID.Hoang, "Player Move");
        }
    }
}
