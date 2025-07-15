using System;
using UnityEngine;

public class triiger_svintus_eat : MonoBehaviour
{
    [SerializeField] private GameObject _cam;
    [SerializeField] private GameObject _camAN;
    [SerializeField] private Animator svin;
    [SerializeField] private Animator deer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _cam.SetActive(false);
            _camAN.SetActive(true);
            svin.CrossFade("svin_eat", 0, 0);
            deer.CrossFade("deer_fall", 0, 0);
        }
    }
}
