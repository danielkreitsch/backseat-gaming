using UnityEngine;

public class StickManController : MonoBehaviour
{
  public _Muscle[] muscles = new _Muscle [7];

  [HideInInspector]
  public bool right;

  [HideInInspector]
  public bool left;

  [HideInInspector]
  public bool CanControllerNow = true;

  public GameObject hip;

  [Header("Movement")]
  [Space]
  public Rigidbody2D rbRight;

  public Rigidbody2D rbLeft;

  public Vector2 WalkRightVector = new(60, 0);
  public Vector2 WalkLeftVector = new(-60, 0);

  float MoveDelayPointer;

  [Range(0.1f, 1)]
  public float MoveDelay = 0.2f;

  private KeyCode leftInput = KeyCode.LeftArrow;
  private KeyCode rightInput = KeyCode.RightArrow;

  private void Start()
  {
    // muscles = GetComponentsInChildren<Rigidbody2D>();
  }

  private void Update()
  {
    foreach (_Muscle muscle in muscles)
    {
      muscle.ActiveMuscle();
    }
    if (CanControllerNow)
    {
      if (Input.GetKeyDown(rightInput) && Input.GetKey(KeyCode.LeftShift))
      {
        right = true;
      }
      if (Input.GetKeyDown(leftInput) && Input.GetKey(KeyCode.LeftShift))
      {
        left = true;
      }
      if (Input.GetKeyUp(leftInput))
      {
        left = false;
        right = false;
      }
      if (Input.GetKeyUp(rightInput))
      {
        left = false;
        right = false;
      }
    }

    while (right == true && left == false && Time.time > MoveDelayPointer)
    {
      Invoke("Step1Right", 0f);
      Invoke("Step2Right", 0.085f);
      MoveDelayPointer = Time.time + MoveDelay;
    }
    while (left == true && right == false && Time.time > MoveDelayPointer)
    {
      Invoke("Step1Left", 0f);
      Invoke("Step2Left", 0.085f);
      MoveDelayPointer = Time.time + MoveDelay;
    }
  }

  public void Step1Right()
  {
    rbRight.AddForce(WalkRightVector, ForceMode2D.Impulse);
    rbLeft.AddForce(WalkRightVector * -0.5f, ForceMode2D.Impulse);
  }

  public void Step2Right()
  {
    rbLeft.AddForce(WalkRightVector, ForceMode2D.Impulse);
    rbRight.AddForce(WalkRightVector * -0.5f, ForceMode2D.Impulse);
  }

  public void Step1Left()
  {
    rbRight.AddForce(WalkLeftVector, ForceMode2D.Impulse);
    rbLeft.AddForce(WalkLeftVector * -0.5f, ForceMode2D.Impulse);
  }

  public void Step2Left()
  {
    rbLeft.AddForce(WalkLeftVector, ForceMode2D.Impulse);
    rbRight.AddForce(WalkLeftVector * -0.5f, ForceMode2D.Impulse);
  }
}

[System.Serializable]
public class _Muscle
{
  public Rigidbody2D bone;
  public float RootRotation;
  public float force = 1000f;

  public void ActiveMuscle()
  {
    bone.MoveRotation(Mathf.LerpAngle(bone.rotation, RootRotation, force * Time.deltaTime));
  }

  public _Muscle(Transform t)
  {
    bone = t.GetComponent<Rigidbody2D>();
  }
}
