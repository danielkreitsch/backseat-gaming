using UnityEngine;

namespace Project
{
  [CreateAssetMenu(fileName = "ApplicationSettings", menuName = "Project/ApplicationSettings")]
  public class ApplicationSettings : ScriptableObject
  {
    [SerializeField]
    private string loadingScreenScene;

    [SerializeField]
    private string[] primaryScenes;

    public string LoadingScreenScene => loadingScreenScene;

    public string[] PrimaryScenes => primaryScenes;
  }
}
