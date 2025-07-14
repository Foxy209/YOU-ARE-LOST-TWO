using UnityEngine;

public class PlHeart : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    
    private void Start() => currentHealth = maxHealth;
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) Die();
    }
    
    public void Die()
    {
        CheckpointSystem.Instance.RespawnPlayer(gameObject);
    }
    
    public void ResetToCheckpoint()
    {
        currentHealth = maxHealth;
    }
}
