using System.Collections.Generic;

namespace HoangHH
{
    public enum DevID
    {
        Hoang,
    }
    
    public static class HLog
    {
        private static readonly List<string> LogColors = new()
        {
            "#FF0000",
        }; // Add Color for DevID here
// #if UNITY_EDITOR
        public static void Log(DevID id, string log)
        {
            UnityEngine.Debug.Log($"<color={LogColors[(int)id]}>[{id}] {log}</color>");
        }
// #endif
    }
}
