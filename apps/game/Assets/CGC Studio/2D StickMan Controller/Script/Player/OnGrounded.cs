using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGrounded : MonoBehaviour
{
  public GameObject PlayerHip;
  public GameObject Dust;

  void OnCollisionEnter2D(Collision2D col)
  {
    if (col.gameObject.tag == "Ground")
    {
      PlayerHip.GetComponent<Jump>().Grounded = true;
      // Instantiate(Dust, transform.position, transform.rotation);
    }
  }

  void OnCollisionStay2D(Collision2D col)
  {
    if (col.gameObject.tag == "Ground")
    {
      PlayerHip.GetComponent<Jump>().Grounded = true;
    }
  }

  void OnCollisionExit2D(Collision2D col)
  {
    if (col.gameObject.tag == "Ground")
    {
      PlayerHip.GetComponent<Jump>().Grounded = false;
      // Instantiate(Dust, transform.position, transform.rotation);
    }
  }
}
