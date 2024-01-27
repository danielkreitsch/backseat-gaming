using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace Project
{
  public class BackendService : MonoBehaviour
  {
    [Inject]
    private ApplicationManager applicationManager;

    private string ApiUrl => applicationManager.LocalApi ? applicationManager.LocalApiUrl : applicationManager.RemoteApiUrl;

    public IEnumerator GeneratePlayerInput(string request, Action<string, List<GameAction>> onSuccess, Action<string, string> onFailure)
    {
      string prompt = request.Replace("\n", " ").Replace("\r", " ");

      using (UnityWebRequest webRequest = UnityWebRequest.Get($"{ApiUrl}/generatePlayerInput?request={prompt}"))
      {
        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError || webRequest.isHttpError)
        {
          onFailure?.Invoke(prompt, webRequest.error);
        }
        else
        {
          List<GameAction> actions = DeserializeGameActions(webRequest.downloadHandler.text);
          onSuccess?.Invoke(prompt, actions);
        }
      }
    }

    private List<GameAction> DeserializeGameActions(string jsonResponse)
    {
      return JsonConvert.DeserializeObject<List<GameAction>>(jsonResponse);
    }
  }
}
