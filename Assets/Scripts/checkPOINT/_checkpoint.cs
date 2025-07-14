using UnityEngine;

public class _checkpoint : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private Light checkpointLight;
    [SerializeField] private Color activatedColor = Color.green;

    private bool isActivated = false;
    private Color originalColor;

    private void Start()
    {
        if (checkpointLight != null)
            originalColor = checkpointLight.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FPSPlayer") && !isActivated)
        {
            isActivated = true;
            CheckpointSystem.Instance.SetCheckpoint(
                respawnPoint.position, 
                respawnPoint.rotation,
                gameObject
            );
            
            if (checkpointLight != null)
                checkpointLight.color = activatedColor;
        }
    }
}
