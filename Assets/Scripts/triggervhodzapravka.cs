using UnityEngine;

public class triggervhodzapravka : MonoBehaviour
{
    public Vector3 targetPosition;
    public GameObject igrok;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameController"))
        {
            igrok.transform.position = targetPosition;
            Debug.Log("234234");
        }
    }
}

