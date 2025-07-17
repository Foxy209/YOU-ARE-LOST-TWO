using System;
using UnityEngine;

public class zagl_scene_svin_before : MonoBehaviour
{
    [SerializeField] private GameObject wall_block;
    [SerializeField] private GameObject svin;
    [SerializeField] private bool wall;

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
                svin.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}
