using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.UI
{
  public class PromptUIHandler : MonoBehaviour
  {
    [Inject]
    private ApplicationManager applicationManager;

    [Inject]
    private BackendService backendService;

    [Inject]
    private GeneratedPlayerInputHandler generatedPlayerInputHandler;

    [SerializeField]
    private TMP_InputField promptInputField;

    [SerializeField]
    private TMP_InputField invisibleInputField;

    [SerializeField]
    private Button sendPromptButton;

    [SerializeField]
    private TMP_Text actionsDisplayText;

    void Start()
    {
      sendPromptButton.onClick.AddListener(OnSend);
      promptInputField.onValidateInput += HandleInput;

      promptInputField.Select();
    }

    private char HandleInput(string text, int charIndex, char addedChar)
    {
      if (addedChar == '\n')
      {
        OnSend();
        return '\0'; // Do not add the newline character
      }
      return addedChar; // Add the character normally
    }

    private void OnSend()
    {
      string prompt = promptInputField.text;

      if (applicationManager.DebugMode)
      {
        actionsDisplayText.text = $"Prompt:\n{prompt}\n\n";
      }

      SendPromptToServer(prompt);
      promptInputField.text = "";
      invisibleInputField.Select();
      promptInputField.Select();
    }

    private void SendPromptToServer(string prompt, int tries = 0)
    {
      StartCoroutine(backendService.GeneratePlayerInput(prompt, OnGeneratePlayerInputSuccess, (prompt, error) =>
      {
        if (tries < 3)
        {
          SendPromptToServer(prompt, tries + 1);
        }
        else
        {
          HandleError(prompt, error);
        }
      }));
    }

    private void OnGeneratePlayerInputSuccess(string prompt, List<GameAction> actions)
    {
      DisplayActions(actions);
      generatedPlayerInputHandler.HandleInput(actions);
    }

    private void DisplayActions(List<GameAction> actions)
    {
      if (applicationManager.DebugMode)
      {
        actionsDisplayText.text += "Actions:\n";

        foreach (var action in actions)
        {
          actionsDisplayText.text += $"- {action.type.ToString()}" + (action.argument != null ? $"({action.argument})" : "()") + "\n";
        }
      }
    }

    private void HandleError(string prompt, string error)
    {
      actionsDisplayText.text += $"Error:\n{error}\n";
      Debug.LogError(error);
    }
  }
}
