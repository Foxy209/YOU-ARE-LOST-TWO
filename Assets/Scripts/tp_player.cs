using System;
using UnityEngine;

public class tp_player : MonoBehaviour
{
   [SerializeField] private GameObject pl;
   [SerializeField] private GameObject tp;


   private void Update()
   {
      CharacterController cc = pl.GetComponent<CharacterController>();
      if (cc != null) cc.enabled = false;
    
      pl.transform.position = tp.transform.position;
      pl.transform.rotation = tp.transform.rotation;
      if (cc != null) cc.enabled = true;
      Destroy(gameObject);
   }
}
