using System;
using UnityEngine;

public class triiger_svintus_eat : MonoBehaviour
{
    [SerializeField] private GameObject _cam;
    [SerializeField] private GameObject _camAN;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject spawnPlayer;
    [SerializeField] private GameObject carRide;
    [SerializeField] private GameObject carStatic;
    [SerializeField] private GameObject SvinRun;
    [SerializeField] private GameObject SvinEat;
    [SerializeField] private AudioSource svinSound;
    [SerializeField] private Animator svin;
    [SerializeField] private Animator deer;
    [SerializeField] private Animator door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _cam.SetActive(false);
            _camAN.SetActive(true);
            svinSound.Play();
            svin.CrossFade("svin_eat", 0, 0);
            deer.CrossFade("deer_fall", 0, 0);
            door.CrossFade("door_open_svin", 0, 0);
            Invoke(nameof(before), 15);
        }
    }

    private void before()
    {
        Player.transform.position = spawnPlayer.transform.position;
        carStatic.transform.position = carRide.transform.position;
        carStatic.transform.rotation = carRide.transform.rotation;
        carRide.SetActive(false);
        carStatic.SetActive(true);
        Player.SetActive(true);
        SvinRun.SetActive(true);
        SvinEat.SetActive(false);
        _camAN.SetActive(false);
    }
}
