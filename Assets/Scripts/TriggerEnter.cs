using UnityEngine;

public class TriggerEnter : MonoBehaviour
{
    [SerializeField] private string Tag; 
    
    [SerializeField] private bool DoYouNeedUsingSound;
    [SerializeField] private AudioSource sour_using;
    [SerializeField] private bool DoYouNeedToChangeClip;
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioSource sour_change;

    [SerializeField] private bool _enable_GM;
    [SerializeField] private bool _disable_GM;
    [SerializeField] private GameObject[] enabl;
    [SerializeField] private GameObject[] disabl;
    
    [SerializeField] private bool DoYouNeedAnim;
    [SerializeField] private Animator zatemn;
    [SerializeField] private string zatem_who;
    private bool inTrigger = false;

    private void Update()
    {
        if (inTrigger)
        {
            foreach (GameObject obj in enabl)
            {
                if(obj != null) obj.SetActive(_enable_GM);
            }
            foreach (GameObject obj in disabl)
            {
                if(obj != null) obj.SetActive(!_disable_GM);
            }

            if (DoYouNeedAnim)
            {
                zatemn.CrossFade(zatem_who,0,0);
            }
            
            if (DoYouNeedUsingSound)
            {
                sour_using.Play();
            }
            if (DoYouNeedToChangeClip)
            {
                sour_change.clip = clip;
                sour_change.Play();
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag))
        {
            inTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tag))
        {
            inTrigger = false;
        }
    }
}
