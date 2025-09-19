using System;
using UnityEngine;

public class zagl_scene_svin_before : MonoBehaviour
{
    [SerializeField] private GameObject wall_block;
    [SerializeField] private GameObject svin;
    [SerializeField] private bool wall;
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioSource sour_change;
    public Trigger_smert_ot_svin smena;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FPSPlayer"))
        {
            if (wall)
            {
                wall_block.SetActive(true);
                Destroy(gameObject);   
            }
            else
            {
                smena.smertTWO = true;
                svin.SetActive(false);
                sour_change.clip = clip;
                sour_change.Play();
                Destroy(gameObject);
            }
        }
    }
}
