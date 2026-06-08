using System;
using UnityEngine;

public class cutscenePEREHOD : MonoBehaviour
{
   [SerializeField] private GameObject pl;
   [SerializeField] private GameObject pl_tp;
   [SerializeField] private GameObject shotgunSCENE;
   [SerializeField] private GameObject gas;
   [SerializeField] private GameObject sceneFIRST;
   [SerializeField] private GameObject sceneFIRSTUI;
   [SerializeField] private GameObject DoorStatic;
   [SerializeField] private GameObject flashOBJ;
   [SerializeField] private GameObject flashLIGHT;
   [SerializeField] private int timefirstScene_before;
   [SerializeField] private int timesecondScene;
   [SerializeField] private int time_gas;
   [SerializeField] private AudioClip clip;
   [SerializeField] private AudioSource sour_change;
   private void Start()
   {
      Invoke(nameof(BeforeScene), timefirstScene_before);
   }

   private void BeforeScene()
   {
      CharacterController cc = pl.GetComponent<CharacterController>();
      if (cc != null) cc.enabled = false;
    
      pl.transform.position = pl_tp.transform.position;
      pl.transform.rotation = pl_tp.transform.rotation;
    
      if (cc != null) cc.enabled = true;
      Destroy(flashOBJ);
      Destroy(flashLIGHT);
      pl.SetActive(true);
      sour_change.clip = clip;
      sour_change.Play();
      Destroy(sceneFIRST);
      Invoke(nameof(startGAS), time_gas);
   }

   private void startGAS()
   {
      gas.SetActive(true);
      Invoke(nameof(startsecondSCENE), timesecondScene);
      
   }

   private void startsecondSCENE()
   {
      Destroy(sceneFIRSTUI);
      Destroy(DoorStatic);
      shotgunSCENE.SetActive(true);
   }
   
   
}
