using System;
using UnityEngine;

public class zagl_open_first_door : MonoBehaviour
{
    [SerializeField] private GameObject cl_door;
    [SerializeField] private GameObject op_door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FPSPlayer"))
        {
            cl_door.SetActive(false);
            op_door.SetActive(true);
        }
    }
}
