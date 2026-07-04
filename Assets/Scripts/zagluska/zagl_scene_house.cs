using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class zagl_scene_house : MonoBehaviour
{
    [SerializeField] private GameObject pool;
    [SerializeField] private GameObject luke_under;
    [SerializeField] private GameObject luke;
    [SerializeField] private AudioSource first_door;
    [SerializeField] private AudioSource two_door;
    [SerializeField] private AudioSource mesdu_vlom;

    private void Update()
    {
        Invoke(nameof(end), 20);
    }

    
    private void end()
    {
        pool.SetActive(false);
        luke_under.SetActive(true);
        luke.SetActive(false);
        Invoke(nameof(perenos), 2);
    }

    private void perenos()
    {
        SceneManager.LoadScene("lab");
    }
}
