using System;
using UnityEngine;

public class zagl_dial_mp3LAB : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject pl;
    [SerializeField] private GameObject cutscrnr;

    private void Update()
    {
        if (panel.activeSelf == false)
        {
            pl.SetActive(false);
            cutscrnr.SetActive(true);
            Destroy(gameObject);
        }
    }
}
