using System;
using EvolveGames;
using UnityEngine;

public class DialGiver : MonoBehaviour
{
    [SerializeField] private GameObject dialpan;
    [SerializeField] private GameObject igrok;
    private bool isdlcompl = false; 
    [SerializeField] private string tagtouse;
    [SerializeField] private Sprite[] spkrigs;
    [SerializeField] private string[] spkrnms;
    [SerializeField] private string[] spkrxts;
    [SerializeField] private AudioClip[] txtsnds;
    [SerializeField] private bool _dopOn;
    [SerializeField] private bool _dopOff;
    [SerializeField] private GameObject dopOn;
    [SerializeField] private GameObject dopOff;
    
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
            igrok.GetComponent<PlayerController>().RuningSpeed = 0f;
            igrok.GetComponent<PlayerController>().walkingSpeed = 0f;
            igrok.GetComponent<PlayerController>().CroughSpeed = 0f;

            if (_dopOn)
            {
                dopOn.SetActive(true);
            }
            if (_dopOff)
            {
                dopOff.SetActive(false);
            }
        }
    }
}
