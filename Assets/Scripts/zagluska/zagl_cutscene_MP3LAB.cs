using System;
using UnityEngine;

public class zagl_cutscene_MP3LAB : MonoBehaviour
{
   [SerializeField] private GameObject tp;
   [SerializeField] private GameObject pl;
   [SerializeField] private GameObject monsters;
   [SerializeField] private GameObject trig_openDoor;
   [SerializeField] private int time;
   [SerializeField] private AudioClip clip;
   [SerializeField] private AudioSource sour_change;
   private void Update()
   {
      Invoke(nameof(endCut), time);
   }

   private void endCut()
   {
      trig_openDoor.SetActive(true);
      pl.transform.position = tp.transform.position;
      pl.transform.rotation = tp.transform.rotation;
      pl.SetActive(true);
      Destroy(tp);
      sour_change.clip = clip;
      sour_change.Play();
      
      monsters.SetActive(true);
   }
}
