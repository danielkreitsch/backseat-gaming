using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
  public PlayerDead Player;

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag(Player.DeadTag))
    {
      Player.IsDead = true;
    }
  }
}
