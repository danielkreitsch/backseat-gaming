using UnityEngine;
using Zenject;

namespace Project
{
  public class MainMenuController : MonoBehaviour
  {
    [Inject]
    private ApplicationManager applicationManager;

    public void Play()
    {
      applicationManager.SwitchPrimaryScene(1);
    }
  }
}
