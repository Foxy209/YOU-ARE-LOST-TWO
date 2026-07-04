using System;
using UnityEngine;

public class smertLAB : MonoBehaviour
{
   [SerializeField] private GameObject smert1;
   [SerializeField] private GameObject smert2;
   [SerializeField] private GameObject pl;
   [SerializeField] private CharacterController Mpl;
   [SerializeField] private GameObject monst1;
   [SerializeField] private GameObject monst2;
   
   [SerializeField] private GameObject check;
   
   [SerializeField] private GameObject monstTP;
   [SerializeField] private GameObject plTP1;
   [SerializeField] private GameObject plTP2;

   private void Update()
   {
      if (smert1.activeSelf)
      {
         Cursor.lockState = CursorLockMode.Locked;
         Cursor.visible = false;
         pl.transform.position = plTP1.transform.position;
         Mpl.enabled = true;
         monst1.SetActive(true);
         smert1.SetActive(false);
         check.SetActive(false);
      }

      if (smert2.activeSelf)
      {
         Cursor.lockState = CursorLockMode.Locked;
         Cursor.visible = false;
         pl.transform.position = plTP2.transform.position;
         monst2.transform.position = monstTP.transform.position;
         Mpl.enabled = true;
         monst2.SetActive(true);
         smert2.SetActive(false);
         check.SetActive(false);
      }
   }
}
