using UnityEngine;
using System.Collections;
public class trigg_WallBreake : MonoBehaviour
{
     [Header("Блоки стены")]
    [SerializeField] private Rigidbody[] wallBlocks;

    [Header("Настройки разлёта")]
    [SerializeField] private float blockForce = 15f;
    [SerializeField] private float blockUpForce = 3f;
    [SerializeField] private float blockSpread = 2f;
    [SerializeField] private float disableCollidersDelay = 0.3f; // через сколько отключить коллайдеры

    [Header("Монстр")]
    [SerializeField] private GameObject monster;
    [SerializeField] private float monsterRunDelay = 0.5f;

    [Header("Звук")]
    [SerializeField] private AudioSource breakSound;

    private bool triggered;

    void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("FPSPlayer"))
        {
            triggered = true;
            StartCoroutine(BreakWall());
        }
    }

    IEnumerator BreakWall()
    {
        if (breakSound != null) breakSound.Play();

        // Разбрасываем блоки
        foreach (Rigidbody rb in wallBlocks)
        {
            if (rb != null)
            {
                rb.isKinematic = false;

                Vector3 randomDir = transform.forward + Random.insideUnitSphere * blockSpread;
                randomDir.y = Mathf.Abs(randomDir.y);
                randomDir.Normalize();

                rb.AddForce(randomDir * blockForce, ForceMode.Impulse);
                rb.AddTorque(Random.insideUnitSphere * 5f, ForceMode.Impulse);
            }
        }

        // Через небольшое время отключаем коллайдеры на блоках
        yield return new WaitForSeconds(disableCollidersDelay);

        foreach (Rigidbody rb in wallBlocks)
        {
            if (rb != null)
            {
                Collider col = rb.GetComponent<Collider>();
                if (col != null)
                    col.enabled = false; // отключаем коллайдер
            }
        }

        // Ждём и выпускаем монстра
        yield return new WaitForSeconds(monsterRunDelay - disableCollidersDelay);

        if (monster != null)
        {
            monster.SetActive(true);

            AI_presledov chaser = monster.GetComponent<AI_presledov>();
            if (chaser != null)
            {
                chaser.enabled = true;
            }
        }

        gameObject.SetActive(false);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
