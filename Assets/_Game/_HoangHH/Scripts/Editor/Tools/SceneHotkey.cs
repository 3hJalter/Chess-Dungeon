using UnityEditor;
using UnityEditor.SceneManagement;

namespace HoangHH._Game._HoangHH.Scripts.Editor.Tools
{
    public static class SceneHotkey
    {
        [MenuItem("HoangHH/Scenes/GameDemo")]
        private static void OpenGameDemoScene()
        {
            OpenScene("GameDemo");
        }
        
        // Add more scenes here
        
        private static void OpenScene(string sceneName)
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorSceneManager.OpenScene($"Assets/_Game/_HoangHH/Scenes/{sceneName}.unity");
            }
        }
    }
}
