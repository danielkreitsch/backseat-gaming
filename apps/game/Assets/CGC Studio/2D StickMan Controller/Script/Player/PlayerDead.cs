using UnityEngine;
using UnityEngine.Events;

public class PlayerDead : MonoBehaviour
{
  public string DeadTag;
  public UnityEvent OnPlayerDead;

  public bool IsDead { get; set; }

  public bool IsDeadTriggered { get; private set; }

  private void Update()
  {
    if (IsDead && !IsDeadTriggered)
    {
      IsDeadTriggered = true;
      OnPlayerDead?.Invoke();
    }
  }
}
