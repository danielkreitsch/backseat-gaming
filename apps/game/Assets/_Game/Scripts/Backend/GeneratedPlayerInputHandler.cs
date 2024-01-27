using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Project
{
  public class GeneratedPlayerInputHandler : MonoBehaviour
  {
    private Dictionary<GameActionType, Func<string, IEnumerator>> actionMap;

    private Coroutine actionsCoroutine;

    [Inject]
    private GameActionExecutor executor;

    private void Start()
    {
      actionMap = new Dictionary<GameActionType, Func<string, IEnumerator>>
      {
        { GameActionType.None, executor.None },
        { GameActionType.Left, arg => executor.MoveLeft(float.Parse(arg)) },
        { GameActionType.Right, arg => executor.MoveRight(float.Parse(arg)) },
        { GameActionType.Jump, arg => executor.Jump(float.Parse(arg)) },
        { GameActionType.Wait, arg => executor.Wait(float.Parse(arg)) },
        { GameActionType.Respond, arg => executor.Respond(arg) },
        { GameActionType.Execute, arg => executor.Execute(arg) }
      };
    }

    public void HandleInput(List<GameAction> actions)
    {
      if (actionsCoroutine != null)
      {
        StopCoroutine(actionsCoroutine);
      }
      actionsCoroutine = StartCoroutine(HandleInput_Co(actions));
    }

    public void OnPlayerDead()
    {
      if (actionsCoroutine != null)
      {
        StopCoroutine(actionsCoroutine);
      }
    }

    private IEnumerator HandleInput_Co(List<GameAction> actions)
    {
      foreach (var action in actions)
      {
        Debug.Log($"Executing action: {action.type} with argument: {action.argument}");
        if (actionMap.TryGetValue(action.type, out var actionCoroutine))
        {
          yield return StartCoroutine(actionCoroutine(action.argument));
        }
        else
        {
          Debug.LogError($"No action found for type: {action.type}");
        }
      }
    }
  }
}
