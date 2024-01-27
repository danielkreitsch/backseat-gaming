using UnityEngine;
using Zenject;

namespace Project
{
  public class GameInstaller : MonoInstaller
  {
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private GeneratedPlayerInputHandler playerInputHandler;

    [SerializeField]
    private GameActionExecutor gameActionExecutor;

    public override void InstallBindings()
    {
      Container.Bind<GameManager>().FromInstance(gameManager).AsSingle();
      Container.Bind<PlayerController>().FromInstance(playerController).AsSingle();
      Container.Bind<GeneratedPlayerInputHandler>().FromInstance(playerInputHandler).AsSingle();
      Container.Bind<GameActionExecutor>().FromInstance(gameActionExecutor).AsSingle();
    }
  }
}
