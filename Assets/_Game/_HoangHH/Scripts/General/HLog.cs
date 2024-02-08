using System.Collections.Generic;

namespace HoangHH
{
    public enum DevID
    {
            
    }
    
    public class HLog
    {
        private static readonly List<string> LogColors = new()
        {
            
        }; // Add Color for DevID here
#if UNITY_EDITOR
        public static void Log(DevID id, string log)
        {
            UnityEngine.Debug.Log($"<color={LogColors[(int)id]}>[{id}] {log}</color>");
        }
#endif
    }
}
