using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Jump : MonoBehaviour
{
  public bool Grounded;

  void Update()
  {
    // Manual input
    if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.UpArrow) && Grounded)
    {
      JumpUp(10);
    }
  }

  public void JumpUp(float force)
  {
    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, force), ForceMode2D.Impulse);
  }
}
