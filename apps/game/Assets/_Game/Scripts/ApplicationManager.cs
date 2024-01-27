using DanielUtils;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project
{
  public class ApplicationManager : MonoBehaviour
  {
    [SerializeField]
    private ApplicationSettings settings;

    [SerializeField]
    private SceneLoader sceneLoader;

    [SerializeField]
    private bool loadStartSceneOnAwake = true;

    [SerializeField]
    private bool debugMode = false;

    [SerializeField]
    private bool localApi = false;

    [SerializeField]
    private string localApiUrl = "http://localhost:8080/api";

    [SerializeField]
    private string remoteApiUrl = "https://backseatgaming-backend.danielkreitsch.com/api";

    public bool DebugMode => debugMode;

    public bool LocalApi => localApi;

    public string LocalApiUrl => localApiUrl;

    public string RemoteApiUrl => remoteApiUrl;

    private void Awake()
    {
      if (loadStartSceneOnAwake)
      {
        SwitchPrimaryScene(0);
      }
    }

    private void Update()
    {
      if (Input.GetKeyDown(KeyCode.Escape))
      {
        if (sceneLoader.CurrentScene == "MainMenu")
        {
          Application.Quit();
        }
        else
        {
          SwitchPrimaryScene(0);
        }
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
