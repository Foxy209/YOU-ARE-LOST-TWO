using UnityEngine;

public class DialGiver : MonoBehaviour
{
    [SerializeField] private GameObject dialpan;
    private bool isdlcompl = false; 
    [SerializeField] private string tagtouse;
    [SerializeField] private Sprite[] spkrigs;
    [SerializeField] private string[] spkrnms;
    [SerializeField] private string[] spkrxts;
    [SerializeField] private AudioClip[] txtsnds;
    void GiveDial()
    {
        if(!isdlcompl) 
        {
            dialpan.GetComponent<Dial>().spkrimgsF = spkrigs;
            dialpan.GetComponent<Dial>().spkrnamsF = spkrnms;
            dialpan.GetComponent<Dial>().spkrtxtsF = spkrxts;
            dialpan.GetComponent<Dial>().txtsndF = txtsnds;
            dialpan.SetActive(true);
            dialpan.GetComponent<Dial>().Strdial();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(tagtouse) && !isdlcompl) 
        {
            GiveDial();
            isdlcompl = true;
        }
    }
}
