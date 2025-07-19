using System;
using UnityEngine;

public class zagl_dial_midle_door : MonoBehaviour
{
    [SerializeField] private GameObject door_close;
    [SerializeField] private GameObject door_open;
    [SerializeField] private GameObject hand;
    [SerializeField] private GameObject triggerE;
    [SerializeField] private GameObject Panel;

    private void Update()
    {
        if (Panel == false)
        {
            triggerE.SetActive(true);
            door_close.SetActive(false);
            door_open.SetActive(true);
            hand.SetActive(true);
        }
    }
}
