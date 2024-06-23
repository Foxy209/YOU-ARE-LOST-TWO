using UnityEngine;

public class trigerinakarte : MonoBehaviour
{
    [SerializeField] private GameObject gryzovik_movment;    
    [SerializeField] private GameObject gryzovik_static;    
    [SerializeField] private GameObject igrok;
    [SerializeField] private GameObject zatem;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            zatem.GetComponent<Animator>().CrossFade("zatemnenie",0,0);
            Invoke(nameof(trig), 1);
        }
    }

    private void trig()
    {
        Destroy(gryzovik_movment);
        gryzovik_static.SetActive(true);
        igrok.SetActive(true);
    }
}

