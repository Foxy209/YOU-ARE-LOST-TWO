using System;
using UnityEngine;

public class PCon : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private GameObject pc;
    [SerializeField] private MonoBehaviour playerController;
    [SerializeField] private int timeZast;
    [SerializeField] private int timeCams;
    [SerializeField] private GameObject zastavka;
    [SerializeField] private GameObject cctv;
    [SerializeField] private GameObject cctvUI;
    [SerializeField] private GameObject DoorSTAT1;
    [SerializeField] private GameObject DoorSTAT2;
    [SerializeField] private GameObject DoorANIM;
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioSource sour_change;

    private void Update()
    {
        if (pc.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            characterController.enabled = false;
            playerController.enabled = false;
            Invoke(nameof(zast), timeZast);
        }
        else
        {
            sour_change.clip = clip;
            sour_change.Play();
            cctv.SetActive(true);
            Invoke(nameof(cams), timeCams);    
        }
    }

    private void cams()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerController.enabled = true;
        characterController.enabled = true;
        Destroy(cctv);
        Destroy(cctvUI);
        Destroy(pc);
        Destroy(DoorSTAT1);
        Destroy(DoorSTAT2);
        DoorANIM.SetActive(true);
    }
    private void zast()
    {
        Destroy(zastavka);
    }
}
