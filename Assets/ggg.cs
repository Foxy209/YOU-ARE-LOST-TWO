using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ggg : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("FPSPlayer"))
      {
          SceneManager.LoadScene("game");
      }
   }
}
