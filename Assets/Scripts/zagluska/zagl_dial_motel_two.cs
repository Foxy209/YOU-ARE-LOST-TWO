using System;
using UnityEngine;

public class zagl_dial_motel_two : MonoBehaviour
{
    [SerializeField] private GameObject casir_wolk;
    [SerializeField] private GameObject casir_dial;
    [SerializeField] private GameObject panel;

    

    private void Update()
    {
        if (panel.activeSelf == false)
        {
            Destroy(casir_dial);
            casir_wolk.SetActive(true);
        }
    }
}
