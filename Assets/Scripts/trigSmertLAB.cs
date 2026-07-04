using System;
using UnityEngine;

public class trigSmertLAB : MonoBehaviour
{
    [SerializeField] private GameObject monst1;
    [SerializeField] private GameObject monst2;
    [SerializeField] private GameObject ui;
    [SerializeField] private GameObject zagl1;
    [SerializeField] private GameObject zagl2;
    [SerializeField] private CharacterController chPL;

    [SerializeField] private bool sm1;
    [SerializeField] private bool sm2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FPSPlayer"))
        {
            if (sm1)
            {
                monst1.SetActive(false);
                zagl1.SetActive(true);
                ui.SetActive(true);
                chPL.enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            if (sm2)
            {
                monst2.SetActive(false);
                zagl2.SetActive(true);
                ui.SetActive(true);
                chPL.enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}
