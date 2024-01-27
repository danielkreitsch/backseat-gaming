using System.Collections;
using UnityEngine;
using Zenject;

namespace Project
{
  public class GameActionExecutor : MonoBehaviour
  {
    [Inject]
    private PlayerController playerController;

    [SerializeField]
    private GameObject speechBubblePrefab;

    [SerializeField]
    private float jumpForceMultiplier = 1;

    private SpeechBubbleController speechBubble;

    public IEnumerator None(string argument)
    {
      Debug.Log("Executing None (argument: " + argument + ")");
      yield return null;
    }

    public IEnumerator MoveLeft(float time)
    {
      playerController.StickManController.left = true;
      playerController.StickManController.right = false;
      yield return new WaitForSeconds(time * Time.timeScale);
      playerController.StickManController.left = false;
    }

    public IEnumerator MoveRight(float time)
    {
      playerController.StickManController.right = true;
      playerController.StickManController.left = false;
      yield return new WaitForSeconds(time * Time.timeScale);
      playerController.StickManController.right = false;
    }

    public IEnumerator Jump(float force)
    {
      var jumpComp = playerController.StickManController.GetComponentInChildren<Jump>();
      for (float t = 0; t < 5; t += Time.deltaTime)
      {
        if (jumpComp.Grounded)
        {
          jumpComp.JumpUp(force * jumpForceMultiplier);
          break;
        }
        yield return null;
      }
    }

    public IEnumerator Wait(float time)
    {
      yield return new WaitForSeconds(time * Time.timeScale);
    }

    public IEnumerator Respond(string text)
    {
      // TODO: Outsource to SpeechBubbleSpawner

      if (speechBubble)
      {
        Destroy(speechBubble.gameObject);
      }

      var duration = 1 + text.Length * 0.1f;
      var speechBubbbleObj = Instantiate(speechBubblePrefab);
      speechBubble = speechBubbbleObj.GetComponent<SpeechBubbleController>();
      speechBubble.Init(
        playerController.StickManController.hip.transform,
        new Vector3(3, 2.5f, 0),
        text,
        duration
      );
      yield return null;
    }

    public IEnumerator Execute(string functionName)
    {
      Debug.Log($"Executing function: {functionName}");
      if (functionName == "slowMotion")
      {
        Time.timeScale = 0.33f;
        yield return new WaitForSeconds(5f * Time.timeScale);
        Time.timeScale = 1;
      }
      yield return null;
    }
  }
}
