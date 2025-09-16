using System;
using UnityEngine;

public class Trigger_smert_ot_svin : MonoBehaviour
{
    [SerializeField] private GameObject svinAI;
    [SerializeField] private GameObject svinEAT;
    [SerializeField] private GameObject pl;
    [SerializeField] private GameObject zaglONE;
    [SerializeField] private GameObject zaglTWO;
    [SerializeField] private GameObject ui;
    public bool smertTWO = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FPSPlayer"))
        {
            svinAI.SetActive(false);
            pl.SetActive(false);
            svinEAT.SetActive(true);   
            Invoke(nameof(zz), 2);
        }
    }

    private void zz()
    {
        if (smertTWO)
        {
            zaglTWO.SetActive(true);
            ui.SetActive(true);
        }
        else
        {
            zaglONE.SetActive(true);
            ui.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
