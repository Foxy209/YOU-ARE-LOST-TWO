using System;
using UnityEngine;

public class cheking_restarting : MonoBehaviour
{
    [SerializeField] private GameObject zagl_svin1;
    [SerializeField] private GameObject svinTR1;
    [SerializeField] private GameObject zagl_svin2;
    [SerializeField] private GameObject svinTR2;
    [SerializeField] private GameObject car1;
    [SerializeField] private GameObject car2;
    [SerializeField] private GameObject hunter;
    
    
    [SerializeField] private GameObject pl1;
    [SerializeField] private GameObject pl2;
    [SerializeField] private GameObject car;
    [SerializeField] private GameObject svin;

    [SerializeField] private GameObject smer_svin1;
    [SerializeField] private GameObject car_end1;
    [SerializeField] private GameObject car_end2;
    [SerializeField] private GameObject hunt;
    
    
    [SerializeField] private GameObject check;


    private void Update()
    {
        if (zagl_svin1.activeSelf)
        {
            pl1.transform.position = zagl_svin1.transform.position;
            svin.transform.position = svinTR1.transform.position;
            pl1.SetActive(true);
            svin.SetActive(true);
            smer_svin1.SetActive(false);
            zagl_svin1.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            check.SetActive(false);
        }   
    }
}
