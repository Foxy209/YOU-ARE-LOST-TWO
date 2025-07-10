using UnityEngine;

public class Taimer_chego_to : MonoBehaviour
{
   [SerializeField] private int Taimer;
   [SerializeField] private bool _enable_GM;
   [SerializeField] private bool _disable_GM;
   [SerializeField] private GameObject[] enabl;
   [SerializeField] private GameObject[] disabl;
   [SerializeField] private bool need_destr_obj;

   private void Update()
   {
      Invoke(nameof(tt), Taimer);
   }

   private void tt()
   {
      foreach (GameObject obj in enabl)
      {
         if(obj != null) obj.SetActive(_enable_GM);
      }
      foreach (GameObject obj in disabl)
      {
         if(obj != null) obj.SetActive(!_disable_GM);
      }

      if (need_destr_obj)
      {
         Destroy(gameObject);  
      }
   }
}
