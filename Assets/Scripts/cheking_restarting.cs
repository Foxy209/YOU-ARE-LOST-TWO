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
    [SerializeField] private GameObject car_scen;
    [SerializeField] private GameObject hunter;
    
    
    [SerializeField] private GameObject pl1;
    [SerializeField] private GameObject pl2;
    [SerializeField] private GameObject car;
    [SerializeField] private GameObject svin;

    [SerializeField] private GameObject smer_svin1;
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
        if (zagl_svin2.activeSelf)
        {
            pl1.transform.position = zagl_svin2.transform.position;
            svin.transform.position = svinTR2.transform.position;
            pl1.SetActive(true);
            svin.SetActive(true);
            smer_svin1.SetActive(false);
            zagl_svin2.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            check.SetActive(false);
        }
        if (car1.activeSelf)
        {
            car.transform.position = car1.transform.position;
            car.SetActive(true);
            car1.SetActive(false);
            car_scen.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            check.SetActive(false);
        }
        if (car2.activeSelf)
        {
            car.transform.position = car2.transform.position;
            car.SetActive(true);
            car2.SetActive(false);
            car_scen.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            check.SetActive(false);
        }
    }
}
