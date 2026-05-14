using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class zagl_bad_end_prolog : MonoBehaviour
{
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject light;
    [SerializeField] private GameObject start_toch;
    [SerializeField] private GameObject car;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            light.SetActive(false);
            Invoke(nameof(meny), 2);
        }
    }

    private void meny()
    {
        UI.SetActive(true);
        Invoke(nameof(resp), 2);
    }

    private void resp()
    {
        Invoke(nameof(off), 1);
    }

    private void off()
    {
        SceneManager.LoadScene("Game");
    }
}
