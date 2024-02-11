using HoangHH.DesignPattern;
using UnityEngine;

namespace HoangHH.Manager
{
    public enum GameState
    {
        MainMenu,
        InGame,
        Pause,
        GameOver
    }
    
    [DefaultExecutionOrder(-100)]
    public class GameManager : Singleton<GameManager>
    {
        protected override void SingletonAwakened()
        {
            base.SingletonAwakened();
            ApplyAppSetting();
            ReduceScreenResolution();
            VerifyData();
        }

        private static void VerifyData()
        {
            
        }
        
        /// <summary>
        ///   Set the base app setting
        /// </summary>
        private static void ApplyAppSetting()
        {
            Input.multiTouchEnabled = false;
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }
        
        /// <summary>
        ///  Reduce the screen resolution if the screen height is greater than 1280
        /// </summary>
        private static void ReduceScreenResolution()
        {
            const int maxScreenHeight = 1280;
            float ratio = Screen.currentResolution.width / (float)Screen.currentResolution.height;
            if (Screen.currentResolution.height > maxScreenHeight)
                Screen.SetResolution(Mathf.RoundToInt(ratio * maxScreenHeight), maxScreenHeight, true);
        }
        
    }
}
