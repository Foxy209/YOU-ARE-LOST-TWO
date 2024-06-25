using UnityEngine;
using System.Collections;
public class TriggerEnterZapravka : MonoBehaviour
{
    [SerializeField] private GameObject trigg;
    [SerializeField] private Transform spawn;
    [SerializeField] private Animator zatemn;
    private void Awake() => zatemn.StopPlayback();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameController"))
        {
            zatemn.CrossFade("zatemnenie",0,0);
            StartCoroutine(Trig(other.gameObject));
        }
    }
    private IEnumerator Trig(GameObject plr)
    {
        yield return new WaitForSeconds(1);
        plr.GetComponent<CharacterController>().enabled = false;
        plr.transform.position = spawn.position;
        plr.GetComponent<CharacterController>().enabled = true;
        Destroy(trigg);
    }
}
