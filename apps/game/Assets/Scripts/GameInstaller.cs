using UnityEngine;
using Zenject;

namespace Project
{
  public class GameInstaller : MonoInstaller
  {
    [SerializeField]
    private GameManager gameManager;

    public override void InstallBindings()
    {
      Container.Bind<GameManager>().FromInstance(gameManager).AsSingle();
    }
  }
}
