using System;
using UnityEngine;

public class zagl_scene_house : MonoBehaviour
{
    [SerializeField] private GameObject pool;
    [SerializeField] private GameObject luke_under;
    [SerializeField] private GameObject luke;
    [SerializeField] private AudioSource first_door;
    [SerializeField] private AudioSource two_door;
    [SerializeField] private AudioSource mesdu_vlom;

    private void Update()
    {
        Invoke(nameof(firstdoor), 5);
    }

    private void mesd()
    {
        mesdu_vlom.Play();
        Invoke(nameof(mesdTWO), 2);
    }
    private void mesdTWO()
    {
        mesdu_vlom.Play();
        Invoke(nameof(twodoor), 6);
    }
    
    
    private void firstdoor()
    {
        first_door.Play();
        Invoke(nameof(mesd), 5);
        
    }
    private void twodoor()
    {
        two_door.Play();
        Invoke(nameof(end), 2);
    }
    
    
    private void end()
    {
        pool.SetActive(false);
        luke_under.SetActive(true);
        luke.SetActive(false);
    }
}
