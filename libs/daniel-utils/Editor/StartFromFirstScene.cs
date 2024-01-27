using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace DanielUtils.Editor
{
  public class StartFromFirstScene
  {
    [MenuItem("Tools/Daniel Utils/Start From First Scene %#T")]
    public static void StartPlayModeFromFirstScene()
    {
      EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
      EditorSceneManager.OpenScene(SceneUtility.GetScenePathByBuildIndex(0));
      EditorApplication.EnterPlaymode();
    }
  }
}
