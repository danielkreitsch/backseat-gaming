using UnityEngine;
using Zenject;

namespace Project
{
  public class GameController : MonoBehaviour
  {
    [Inject]
    private PlayerController playerController;

    private void Update()
    {
      if (Input.GetKeyDown(KeyCode.R) && Input.GetKey(KeyCode.LeftControl))
      {
        playerController.Respawn();
      }
    }
  }
}
