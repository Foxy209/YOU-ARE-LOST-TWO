using UnityEngine;

public class trigerinakarte : MonoBehaviour
{
    [SerializeField] private GameObject gryzovik;    
    [SerializeField] private GameObject igrok;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gryzovik);
            igrok.SetActive(true);
        }
    }
}

