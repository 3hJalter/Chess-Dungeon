using HoangHH.InGame.Character.Base;

namespace HoangHH.InGame.Character
{
    public class Player : BCharacter, IPlayer
    {
        public override void OnSpawn()
        {
            HLog.Log(DevID.Hoang, "Player OnSpawn");
        }

        public override void OnDespawn()
        {
        }
        
        public void Move()
        {
            HLog.Log(DevID.Hoang, "Player Move");
        }
    }
}
