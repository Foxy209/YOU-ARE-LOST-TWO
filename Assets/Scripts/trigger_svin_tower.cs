using System;
using UnityEngine;

public class trigger_svin_tower : MonoBehaviour
{
    [SerializeField] private Animator svin;
    [SerializeField] private GameObject _svA;
    [SerializeField] private GameObject _svR;
    [SerializeField] private GameObject _pl;
    [SerializeField] private GameObject _pl_next;
    [SerializeField] private GameObject _spwP;
    [SerializeField] private GameObject _spwS;
    [SerializeField] private GameObject wall;
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioSource sour_change;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FPSPlayer"))
        {
            _pl.SetActive(false);
            _svA.SetActive(true);
            svin.CrossFade("svinTOWER", 0, 0);
            Invoke(nameof(before), 11);
        }
    }

    private void before()
    {
        sour_change.clip = clip;
        sour_change.Play();
        _svR.transform.position = _spwS.transform.position;
        _pl_next.transform.position = _spwP.transform.position;
        _pl_next.transform.rotation = _spwP.transform.rotation;
        _pl_next.SetActive(true);
        _svR.SetActive(true);
        _svA.SetActive(false);
        wall.SetActive(true);
        
    }
}
