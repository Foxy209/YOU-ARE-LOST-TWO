using Unity.VisualScripting;
using UnityEngine;

public class TriggerUsing_on_E : MonoBehaviour
{
    [SerializeField] private string Tag; 
    [SerializeField] private bool DoYouNeedUsingSound;
    [SerializeField] private AudioSource sour_using;
    [SerializeField] private bool DoYouNeedToChangeClip;
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioSource sour_change;
    [SerializeField] private GameObject UI_E;
    [SerializeField] private bool _enable_GM;
    [SerializeField] private bool _disable_GM;
    [SerializeField] private GameObject[] enabl;
    [SerializeField] private GameObject[] disabl;

    private bool inTrigger = false;

    private void Update()
    {
        if (inTrigger && Input.GetKeyDown(KeyCode.E))
        {
            foreach (GameObject obj in enabl)
            {
                if(obj != null) obj.SetActive(_enable_GM);
            }
            foreach (GameObject obj in disabl)
            {
                if(obj != null) obj.SetActive(!_disable_GM);
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
            UI_E.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag))
        {
            inTrigger = true;
            if(UI_E != null) UI_E.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tag))
        {
            inTrigger = false;
            if(UI_E != null) UI_E.SetActive(false);
        }
    }
}
