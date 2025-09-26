using System;
using TMPro;
using UnityEngine;

public class Monolog_act : MonoBehaviour
{
    [SerializeField] private TMP_Text show_txt;
    [SerializeField] private string txt;
    [SerializeField] private int Taimer;
    [SerializeField] private string tag;

    [SerializeField] private bool need_sound;
    [SerializeField] private AudioSource svuk;

    [SerializeField] private bool need_obj_act;
    [SerializeField] private GameObject act;
    [SerializeField] private bool need_obj_disb;
    [SerializeField] private GameObject disb;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tag))
        {
            show_txt.text = txt;
            if (need_obj_act)
            {
                act.SetActive(true);
            }
            if (need_obj_disb)
            {
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
