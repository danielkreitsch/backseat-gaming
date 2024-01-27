using System.Collections;
using UnityEngine;

namespace Project
{
  public class PlayerController : MonoBehaviour
  {
    [SerializeField]
    private GameObject stickManPrefab;

    [SerializeField]
    private StickManController stickManController;

    [SerializeField]
    private GeneratedPlayerInputHandler generatedPlayerInputHandler;

    private Vector3 respawnPosition;

    public StickManController StickManController => stickManController;

    private void Start()
    {
      respawnPosition = stickManController.transform.position;
      Respawn();
    }

    public void Respawn()
    {
      Destroy(stickManController.gameObject);
      var newStickManObj = Instantiate(stickManPrefab, respawnPosition, Quaternion.identity);
      stickManController = newStickManObj.GetComponent<StickManController>();
      FindObjectOfType<CGCCameraFollow>().Player = newStickManObj.transform.Find("Hip").gameObject;
      FindObjectOfType<PlayerDead>().OnPlayerDead.AddListener(OnDeath);

      var headSetter = FindObjectOfType<HeadSetter>();
      headSetter.SetRandomCustomization();
    }

    private void OnDeath()
    {
      StartCoroutine(OnDeath_Co());
      generatedPlayerInputHandler.OnPlayerDead();
    }

    private IEnumerator OnDeath_Co()
    {
      yield return new WaitForSeconds(2);
      Respawn();
    }
  }
}
