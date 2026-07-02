using System;
using UnityEngine;

public class MegaDestroy : MonoBehaviour
{
    [SerializeField] private GameObject[] video;
    [SerializeField] private GameObject monst;
    [SerializeField] private GameObject zatem;
    [SerializeField] private int time;
    [SerializeField] private GameObject pl;
    [SerializeField] private GameObject tpToch;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FPSPlayer"))
        {
            Invoke(nameof(elevClose), time);
        }
    }

    private void elevClose()
    {
        foreach (GameObject obj in video)
        {
            Destroy(obj);
        }
        Destroy(monst);
        zatem.SetActive(true);
        Invoke(nameof(tp), 5);
    }

    private void tp()
    {
        CharacterController cc = pl.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;
    
        pl.transform.position = tpToch.transform.position;
        pl.transform.rotation = tpToch.transform.rotation;
        if (cc != null) cc.enabled = true;
    }
}
