using UnityEngine;

public class TriggersOnTheMap : MonoBehaviour
{
    [SerializeField] private GameObject ObjectToDisable;
    [SerializeField] private GameObject ObjectToEnable;
    [SerializeField] private string Tag;    
    [SerializeField] private GameObject igrok;
    [SerializeField] private Vector3 PlayerPosition;
    [SerializeField] private Vector3 PlayerRotation;
    [SerializeField] private bool enableplayer = true;
    [SerializeField] private GameObject zatem;
    [SerializeField] private GameObject block_wall;
    [SerializeField] private AudioSource sound;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag))
        {
            zatem.GetComponent<Animator>().CrossFade("zatemnenie",0,0);
            sound.Play();
            Invoke(nameof(Trig), 2);
        }
    }

    private void Trig()
    {
        igrok.transform.position = PlayerPosition;
        igrok.transform.eulerAngles = PlayerRotation;
        igrok.SetActive(enableplayer);
        ObjectToDisable.SetActive(false);
        ObjectToDisable.transform.position = ObjectToEnable.transform.position;
        ObjectToDisable.transform.rotation = ObjectToEnable.transform.rotation;
        ObjectToEnable.SetActive(true);
        //block_wall.SetActive(true);
    }
}