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

    public override void InstallBindings()
    {
      Container.Bind<MainThreadDispatcher>().FromInstance(mainThreadDispatcher).AsSingle();
      Container.Bind<ApplicationManager>().FromInstance(applicationManager).AsSingle();
    }
  }
}
