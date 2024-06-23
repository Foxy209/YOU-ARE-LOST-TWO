using UnityEngine;
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
            Invoke(nameof(trig), 1);
        }
    }
    private void trig()
    {
        player.transform.position = spawn.transform.position;
        Destroy(trigg);
    }
}
