using System;
using UnityEngine;

public class zagl_trig_end_prolog : MonoBehaviour
{
   [SerializeField] private GameObject car_pl;
   [SerializeField] private GameObject CUTscene;
   [SerializeField] private int taimer;

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         car_pl.SetActive(false);
         CUTscene.SetActive(true);
         Invoke(nameof(end), taimer);
      }
   }

   private void end()
   {
      
   }
}
