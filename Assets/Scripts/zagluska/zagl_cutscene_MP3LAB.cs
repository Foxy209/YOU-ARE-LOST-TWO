using System;
using UnityEngine;

public class zagl_cutscene_MP3LAB : MonoBehaviour
{
   [SerializeField] private GameObject tp;
   [SerializeField] private GameObject pl;
   [SerializeField] private GameObject monsters;
   [SerializeField] private GameObject trig_openDoor;
   [SerializeField] private GameObject trig_zagl;
   [SerializeField] private int time;
   [SerializeField] private AudioClip clip;
   [SerializeField] private AudioSource sour_change;
   private void Start()
   {
      Debug.Log("энд кат бам бам");
      Destroy(trig_zagl);
      trig_openDoor.SetActive(true);
      pl.transform.position = tp.transform.position;
      pl.transform.rotation = tp.transform.rotation;
      pl.SetActive(true);
      sour_change.clip = clip;
      sour_change.Play();
      
      monsters.SetActive(true);
      Destroy(gameObject);
      //Invoke(nameof(endCut), time);
      //Debug.Log("таймер пошел");
   }

   private void endCut()
   {
      Debug.Log("энд кат бам бам");
      Destroy(trig_zagl);
      trig_openDoor.SetActive(true);
      pl.transform.position = tp.transform.position;
      pl.transform.rotation = tp.transform.rotation;
      pl.SetActive(true);
      sour_change.clip = clip;
      sour_change.Play();
      
      monsters.SetActive(true);
      Destroy(gameObject);
   }
}
