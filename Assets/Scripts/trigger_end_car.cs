using System;
using UnityEngine;

public class trigger_end_car : MonoBehaviour
{
    [SerializeField] private GameObject car;
    [SerializeField] private GameObject car_scene;
    [SerializeField] private GameObject ui_end;
    [SerializeField] private GameObject zagl;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            car.SetActive(false);
            car_scene.SetActive(true);
            Invoke(nameof(cutscene), 2);
        }
    }

    private void cutscene()
    {
        ui_end.SetActive(true);
        zagl.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
}
