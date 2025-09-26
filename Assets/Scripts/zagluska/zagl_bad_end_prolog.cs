using System;
using UnityEngine;

public class zagl_bad_end_prolog : MonoBehaviour
{
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject light;
    [SerializeField] private GameObject start_toch;
    [SerializeField] private GameObject car;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            light.SetActive(false);
            Invoke(nameof(meny), 2);
        }
    }

    private void meny()
    {
        UI.SetActive(true);
        Invoke(nameof(resp), 2);
    }

    private void resp()
    {
        car.transform.position = start_toch.transform.position;
        car.transform.rotation = start_toch.transform.rotation;
        light.SetActive(true);
        Invoke(nameof(off), 1);
    }

    private void off()
    {
        UI.SetActive(false);
    }
}
