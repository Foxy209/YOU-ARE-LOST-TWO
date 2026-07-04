using UnityEngine;
using UnityEngine.Events;

public class killerCheck : MonoBehaviour
{
    [SerializeField] private int enemiesToKill = 5;
    
    [SerializeField] private UnityEvent OnAllEnemiesKilled;

    [SerializeField] private Transform tp;
    [SerializeField] private GameObject pl;

    private int killedCount;

    void Start()
    {
        EnemyBase.OnAnyEnemyKilled += OnEnemyKilled;
    }

    void OnEnemyKilled()
    {
        killedCount++;

        if (killedCount >= enemiesToKill)
        {
            CharacterController cc = pl.GetComponent<CharacterController>();
            if (cc != null) cc.enabled = false;
    
            pl.transform.position = tp.transform.position;
            pl.transform.rotation = tp.transform.rotation;
            if (cc != null) cc.enabled = true;
            //EnemyBase.DestroyAllEnemies();
            OnAllEnemiesKilled?.Invoke();
            Invoke(nameof(ClearAllBodies), 3f);
        }
    }
    
    void ClearAllBodies()
    {
        EnemyBase[] enemies = FindObjectsOfType<EnemyBase>();
        foreach (EnemyBase enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
        Debug.Log("Все трупы удалены");
    }

    void OnDestroy()
    {
        EnemyBase.OnAnyEnemyKilled -= OnEnemyKilled;
    }
}
