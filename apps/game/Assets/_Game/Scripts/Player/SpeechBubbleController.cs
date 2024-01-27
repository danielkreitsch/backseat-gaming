using TMPro;
using UnityEngine;

public class SpeechBubbleController : MonoBehaviour
{
  [SerializeField]
  private TMP_Text textMesh;

  private Transform targetTransform;
  private Vector3 targetOffset;

  public void Init(Transform targetTransform, Vector3 targetOffset, string text, float duration = 0)
  {
    this.targetTransform = targetTransform;
    this.targetOffset = targetOffset;
    textMesh.text = text;
    Destroy(gameObject, duration > 0 ? duration : 1 + text.Length / 10f);
  }

  private void LateUpdate()
  {
    if (!transform)
    {
      Destroy(gameObject);
    }
    transform.position = targetTransform.position + targetOffset;
  }
}
