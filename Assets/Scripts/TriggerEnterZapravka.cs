using UnityEngine;
using System.Collections;
public class TriggerEnterZapravka : MonoBehaviour
{
    [SerializeField] private GameObject trigg;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject spawn;
    [SerializeField] private GameObject zatemn;
    private void Start() => zatemn.GetComponent<Animator>().StopPlayback();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameController"))
        {
            zatemn.GetComponent<Animator>().CrossFade("zatemnenie",0,0);
            StartCoroutine(Trig(other.gameObject));
        }
    }
    private IEnumerator Trig(GameObject plr)
    {
        yield return new WaitForSeconds(1);
        plr.GetComponent<CharacterController>().enabled = false;
        while (plr.transform.position != spawn.transform.position)
            plr.transform.position = spawn.transform.position;
        plr.GetComponent<CharacterController>().enabled = true;
        Destroy(trigg);
    }
}
