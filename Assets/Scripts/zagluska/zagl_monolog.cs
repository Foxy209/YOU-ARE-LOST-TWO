using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class zagl_monolog : MonoBehaviour
{
    [SerializeField] private TMP_Text txt;
    [SerializeField] private GameObject pn;
    [SerializeField] private AudioSource source_mp;
    [SerializeField] private AudioSource source_ladder;

    private void Update()
    {
        if (pn.activeSelf)
        {
            if (txt.text == "....")
            {
                source_mp.Play(); 
            }
            if (txt.text == ".....")
            {
                source_ladder.Play(); 
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
}
