using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DanielUtils
{
  public class MainThreadDispatcher : MonoBehaviour
  {
    private readonly Queue<Action> executionQueue = new();
    private bool isProcessing;

    private void Update()
    {
      // Processing queue in a single frame may cause frame drop, so process only one per frame.
      if (!isProcessing && executionQueue.Count > 0)
      {
        lock (executionQueue)
        {
          var action = executionQueue.Dequeue();
          isProcessing = true;
          StartCoroutine(RunActionAndSetProcessingToFalse(action));
        }
      }
    }

    /// <summary>
    ///   Enqueue coroutine to be executed in main thread.
    /// </summary>
    public void Enqueue(IEnumerator coroutine)
    {
      lock (executionQueue)
      {
        executionQueue.Enqueue(() => StartCoroutine(coroutine));
      }
    }

    /// <summary>
    ///   Enqueue action to be executed in main thread.
    /// </summary>
    public void Enqueue(Action action)
    {
      lock (executionQueue)
      {
        executionQueue.Enqueue(action);
      }
    }

    private IEnumerator RunActionAndSetProcessingToFalse(Action action)
    {
      action.Invoke();
      yield return null;
      isProcessing = false;
    }
  }
}
