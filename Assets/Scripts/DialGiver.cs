using EvolveGames;
using UnityEngine;

public class DialGiver : MonoBehaviour
{
    [SerializeField] private GameObject dialpan;
    [SerializeField] private PlayerController player;
    [SerializeField] private MovementEffects playerMoveFX;
    [SerializeField] private HandsHolder playerHandHolder;
    [SerializeField] private HeadBob playerHeadBob;
    private bool isdlcompl = false; 
    [SerializeField] private string tagtouse;
    [SerializeField] private Sprite[] spkrigs;
    [SerializeField] private string[] spkrnms;
    [SerializeField] private string[] spkrxts;
    [SerializeField] private AudioClip[] txtsnds;
    [SerializeField] private bool _dopOn;
    [SerializeField] private bool _dopOff;
    [SerializeField] private GameObject[] dopOns;
    [SerializeField] private GameObject[] dopOffs;
    
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
        if (other.gameObject.CompareTag(tagtouse) && !isdlcompl)
        {
            GiveDial();
            isdlcompl = true;
            playerMoveFX.CanMovementFXF = false;
            playerHandHolder.enabled = false;
            playerHeadBob.EnabledF = false;
            player.enabled = false;
            foreach (GameObject ObjectToEnable in dopOns)
            {
                ObjectToEnable.SetActive(_dopOn);
            }
            foreach (GameObject ObjectToDisable in dopOffs)
            {
                ObjectToDisable.SetActive(!_dopOff);
            }
        }
    }
}
