using UnityEngine;
using System.Collections;

public class Trigger_vilet_voron : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject crowPrefab;
    [SerializeField] private Transform spawnPoint; // Точка появления воронов
    [SerializeField] private Transform targetPoint; 
    [SerializeField] private int crowCount = 5;
    [SerializeField] private float spawnRadius = 2f;
    [SerializeField] private float flySpeed = 3f;
    [SerializeField] private float flyHeight = 5f;
    [SerializeField] private float delayBetweenCrows = 0.2f;

    [Header("Effects")]
    [SerializeField] private AudioClip crowSound;
    [SerializeField] private ParticleSystem spawnParticles;

    private bool triggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (triggered || !other.CompareTag("FPSPlayer")) return;
        
        triggered = true;
        StartCoroutine(SpawnCrows());
    }

    IEnumerator SpawnCrows()
    {
        if (spawnParticles) 
        {
            spawnParticles.transform.position = spawnPoint.position;
            spawnParticles.Play();
        }
        
        for (int i = 0; i < crowCount; i++)
        {
            // Используем spawnPoint вместо transform
            Vector3 spawnPos = spawnPoint.position + 
                              new Vector3(
                                  Random.Range(-spawnRadius, spawnRadius),
                                  0,
                                  Random.Range(-spawnRadius, spawnRadius)
                              );

            GameObject crow = Instantiate(crowPrefab, spawnPos, Quaternion.identity);
            StartCoroutine(CrowFlyRoutine(crow.transform));

            if (crowSound) 
                AudioSource.PlayClipAtPoint(crowSound, spawnPos);
            
            yield return new WaitForSeconds(delayBetweenCrows);
        }
    }

    IEnumerator CrowFlyRoutine(Transform crow)
    {
        float progress = 0f;
        Vector3 startPos = crow.position;
        Vector3 peakPos = startPos + Vector3.up * flyHeight;
        Vector3 endPos = targetPoint.position;

        while (progress < 1f)
        {
            progress += Time.deltaTime * flySpeed;
            
            Vector3 point1 = Vector3.Lerp(startPos, peakPos, progress);
            Vector3 point2 = Vector3.Lerp(peakPos, endPos, progress);
            crow.position = Vector3.Lerp(point1, point2, progress);

            if (progress < 0.98f)
            {
                Vector3 nextPos = Vector3.Lerp(
                    Vector3.Lerp(startPos, peakPos, progress + 0.01f),
                    Vector3.Lerp(peakPos, endPos, progress + 0.01f),
                    progress + 0.01f
                );
                crow.LookAt(nextPos);
            }

            yield return null;
        }

        Destroy(crow.gameObject, 1f);
    }
}
