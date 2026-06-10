using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Spawner : MonoBehaviour
{
        [Header("Префаб врага")]
        [SerializeField] private EnemyBase enemyPrefab;
    
        [Header("Точки спавна")]
        [SerializeField] private List<Transform> spawnPoints;
    
        [Header("Настройки")]
        [SerializeField] private int totalEnemies = 5;
        [SerializeField] private float spawnDelay = 0.5f;
    
        void Start()
        {
            StartCoroutine(SpawnAll());
        }
    
        IEnumerator SpawnAll()
        {
            for (int i = 0; i < totalEnemies; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(spawnDelay);
            }
        }
    
        void SpawnEnemy()
        {
            Transform spawnPoint = spawnPoints.Count > 0
                ? spawnPoints[Random.Range(0, spawnPoints.Count)]
                : transform;
    
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    
        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if (spawnPoints != null)
            {
                foreach (Transform t in spawnPoints)
                {
                    if (t != null)
                        Gizmos.DrawWireSphere(t.position, 0.3f);
                }
            }
        }
}
