using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class zagl_music_endScene : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioSource sour_change;

    private void Start()
    {
        Invoke(nameof(titre), 25);
    }

    private void titre()
    {
        sour_change.clip = clip;
        sour_change.Play();
        Invoke(nameof(Inmeny), 35);
    }

    private void Inmeny()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
