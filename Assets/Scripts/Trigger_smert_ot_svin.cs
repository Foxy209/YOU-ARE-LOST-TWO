using System;
using UnityEngine;

public class Trigger_smert_ot_svin : MonoBehaviour
{
    [SerializeField] private GameObject svinAI;
    [SerializeField] private GameObject svinEAT;
    [SerializeField] private GameObject pl;
    [SerializeField] private GameObject zagl;
    [SerializeField] private GameObject ui;


    private void OnTriggerEnter(Collider other)
    {
        if (CompareTag("FPSPlayer"))
        {
            svinAI.SetActive(false);
            pl.SetActive(false);
            svinEAT.SetActive(true);   
            Invoke(nameof(zz), 2);
        }
    }

    private void zz()
    {
        zagl.SetActive(true);
        ui.SetActive(true);
    }
}
