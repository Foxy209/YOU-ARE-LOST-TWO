using UnityEngine;
using System.Collections;
public class trigger_enter_zapravka : MonoBehaviour
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
            StartCoroutine(trig(other.gameObject));
        }
    }
    private IEnumerator trig(GameObject plr)
    {
        yield return new WaitForSeconds(1);
        while (plr.transform.position != spawn.transform.position)
            plr.transform.position = spawn.transform.position;
        Destroy(trigg);
    }
}
