using System;
using UnityEngine;

public class trigger_enter_zapravka : MonoBehaviour
{
    [SerializeField] private GameObject trigg;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject spawn;
    [SerializeField] private GameObject zatemn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GameController")
        {
            zatemn.SetActive(true);
            player.transform.position = spawn.transform.position;
            Destroy(trigg);
        }
    }
}
