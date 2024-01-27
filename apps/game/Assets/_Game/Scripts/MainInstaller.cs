using DanielUtils;
using UnityEngine;
using Zenject;

namespace Project
{
  public class MainInstaller : MonoInstaller
  {
    [SerializeField]
    private MainThreadDispatcher mainThreadDispatcher;

    [SerializeField]
    private ApplicationManager applicationManager;

    [SerializeField]
    private BackendService backendService;

    public override void InstallBindings()
    {
      Container.Bind<MainThreadDispatcher>().FromInstance(mainThreadDispatcher).AsSingle();
      Container.Bind<ApplicationManager>().FromInstance(applicationManager).AsSingle();
      Container.Bind<BackendService>().FromInstance(backendService).AsSingle();
    }
  }
}
