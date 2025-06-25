using System;
using UnityEngine;

public class zagl_dial_motel_two : MonoBehaviour
{
    [SerializeField] private GameObject casir_wolk;
    [SerializeField] private GameObject casir_dial;

    private void Update()
    {
        if (casir_dial.activeSelf == false)
        {
            casir_wolk.SetActive(true);
            Destroy(gameObject);
        }
    }
}
