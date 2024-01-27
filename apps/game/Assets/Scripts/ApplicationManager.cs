using DanielUtils;
using UnityEngine;

namespace Project
{
  public class ApplicationManager : MonoBehaviour
  {
    [SerializeField]
    private ApplicationSettings settings;

    [SerializeField]
    private SceneLoader sceneLoader;

    [SerializeField]
    private bool LoadStartSceneOnAwake = true;

    private void Awake()
    {
      if (LoadStartSceneOnAwake)
      {
        SwitchPrimaryScene(0);
      }
    }

    public void SwitchPrimaryScene(int index)
    {
      sceneLoader.SwitchScene(
        settings.PrimaryScenes[index],
        settings.LoadingScreenScene
      );
    }
  }
}
