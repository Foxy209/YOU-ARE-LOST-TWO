using UnityEngine;

public class TrigersOnTheMap : MonoBehaviour
{
    [SerializeField] private GameObject gryzovik_movment;    
    [SerializeField] private GameObject gryzovik_static;    
    [SerializeField] private GameObject igrok;
    [SerializeField] private GameObject zatem;
    [SerializeField] private GameObject block_wall;
    [SerializeField] private AudioSource sound;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            zatem.GetComponent<Animator>().CrossFade("zatemnenie",0,0);
            sound.Play();
            Invoke(nameof(Trig), 2);
        }
    }

    private void Trig()
    {
        Destroy(gryzovik_movment);
        gryzovik_static.SetActive(true);
        igrok.SetActive(true);
        block_wall.SetActive(true);
    }
}

