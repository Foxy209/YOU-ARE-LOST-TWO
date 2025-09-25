using System;
using TMPro;
using UnityEngine;

public class Monolog_act : MonoBehaviour
{
    [SerializeField] private TMP_Text show_txt;
    [SerializeField] private string txt;
    [SerializeField] private int Taimer;

    [SerializeField] private bool need_sound;
    [SerializeField] private AudioSource svuk;

    [SerializeField] private bool need_obj;
    [SerializeField] private GameObject act;
    [SerializeField] private GameObject disb;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FPSPlayer"))
        {
            show_txt.text = txt;
            if (need_obj)
            {
                act.SetActive(true);
                disb.SetActive(false);
            }

            if (need_sound)
            {
                svuk.Play();
            }
            
            Invoke(nameof(ending), Taimer);
        }
        
    }

    private void ending()
    {
        show_txt.text = "";
        Destroy(gameObject);
    }
}
