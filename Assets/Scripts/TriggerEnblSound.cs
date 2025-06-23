using System;
using UnityEngine;

public class TriggerEnblSound : MonoBehaviour
{
    [SerializeField] private bool Smena_Soundtrack;
    [SerializeField] private AudioSource _source_SoundTrack;
    [SerializeField] private AudioClip clip;

    [SerializeField] private bool Vrubit_sound;
    [SerializeField] private AudioSource _source_Sound;

    [SerializeField] private string Tag;

    [SerializeField] private bool need_Taimer;
    [SerializeField] private int Taimer;
    [SerializeField] private bool ENBgm;
    [SerializeField] private bool DISgm;
    [SerializeField] private GameObject eng_GM;
    [SerializeField] private GameObject did_GM;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag))
        {
            if (Smena_Soundtrack)
            {
                _source_SoundTrack.clip = clip;
                _source_SoundTrack.Play();
            }

            if (Vrubit_sound)
            {
                _source_Sound.Play();
            }

            if (need_Taimer)
            {
                Invoke(nameof(Tm), Taimer);
            }
            else
            {
                if (ENBgm)
                {
                    eng_GM.SetActive(true);
                }

                if (DISgm)
                {
                    did_GM.SetActive(false);
                }   
                Destroy(gameObject);
            }
        }
    }

    private void Tm()
    {
        if (ENBgm)
        {
            eng_GM.SetActive(true);
        }

        if (DISgm)
        {
            did_GM.SetActive(false);
        }   
        Destroy(gameObject);
    }

}
