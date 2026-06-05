using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
public class EnemyBase : MonoBehaviour, IDamageable, IKnockbackable, ISprayBlood
{
      [Header("Здоровье")]
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    [Header("Кровь")]
    [SerializeField] private ParticleSystem bloodSprayPrefab;
    [SerializeField] private GameObject bloodDecalPrefab; // декаль для стен

    [Header("Отбрасывание")]
    [SerializeField] private float stunTime = 0.5f;

    [Header("Ragdoll")]
    [SerializeField] private Animator anim;
    [SerializeField] private Collider mainCollider; // основной коллайдер врага

    private NavMeshAgent agent;
    private Rigidbody mainRigidbody;
    private List<Rigidbody> ragdollRigidbodies = new List<Rigidbody>();
    private List<Collider> ragdollColliders = new List<Collider>();

    private bool isDead;
    private Vector3 pendingKnockbackForce;
    private bool hasPendingKnockback;
    private bool knockbackAppliedThisFrame;

    void Start()
    {
        currentHealth = maxHealth;
        agent = GetComponent<NavMeshAgent>();
        mainRigidbody = GetComponent<Rigidbody>();

        // Собираем все Rigidbody с костей (ragdoll)
        Rigidbody[] allRbs = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in allRbs)
        {
            if (rb != mainRigidbody)
            {
                ragdollRigidbodies.Add(rb);
                rb.isKinematic = true; // в жизни — анимируются
            }
        }

        // Собираем все коллайдеры с костей
        Collider[] allCols = GetComponentsInChildren<Collider>();
        foreach (Collider col in allCols)
        {
            if (col != mainCollider)
            {
                ragdollColliders.Add(col);
                col.enabled = false; // в жизни — только основной коллайдер
            }
        }

        if (mainRigidbody != null)
        {
            mainRigidbody.isKinematic = true;
            mainRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    void Update()
    {
        knockbackAppliedThisFrame = false;
    }

    void FixedUpdate()
    {
        if (hasPendingKnockback && !knockbackAppliedThisFrame)
        {
            hasPendingKnockback = false;
            knockbackAppliedThisFrame = true;
            ApplyPendingForce();
        }
    }

    void ApplyPendingForce()
    {
        if (agent != null) agent.enabled = false;

        // Если живой — используем основной Rigidbody
        if (!isDead)
        {
            if (mainRigidbody != null)
            {
                mainRigidbody.isKinematic = false;
                mainRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                mainRigidbody.linearVelocity = Vector3.zero;
                mainRigidbody.AddForce(pendingKnockbackForce, ForceMode.Impulse);
            }

            pendingKnockbackForce = Vector3.zero;
            StartCoroutine(RecoverFromKnockback());
        }
        else
        {
            // Если мёртвый — сила уходит в голову или таз (куда попали)
            EnableRagdoll();

            // Даём кадр на включение физики костей
            StartCoroutine(ApplyForceToRagdollNextFrame(pendingKnockbackForce));
            pendingKnockbackForce = Vector3.zero;
        }
    }

    IEnumerator ApplyForceToRagdollNextFrame(Vector3 force)
    {
        yield return null; // ждём кадр, чтобы регдолл включился

        // Находим ближайшую кость к точке попадания или просто бьём в таз
        Rigidbody targetBone = ragdollRigidbodies.Count > 0 ? ragdollRigidbodies[0] : null;
        if (targetBone != null)
        {
            targetBone.AddForce(force, ForceMode.Impulse);
        }

        StartCoroutine(DisableAfterDelay(5f));
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
        else if (anim != null)
        {
            anim.SetTrigger("Hurt");
        }
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        if (anim != null) anim.enabled = false; // ОТКЛЮЧАЕМ АНИМАТОР
        if (agent != null) agent.enabled = false;

        // Отключаем основной коллайдер и Rigidbody
        if (mainCollider != null) mainCollider.enabled = false;
        if (mainRigidbody != null)
        {
            mainRigidbody.isKinematic = true;
            mainRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    void EnableRagdoll()
    {
        // Включаем все коллайдеры и Rigidbody на костях
        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = false;
            rb.linearVelocity = Vector3.zero;
        }
        foreach (Collider col in ragdollColliders)
        {
            col.enabled = true;
        }
    }

    IEnumerator RecoverFromKnockback()
    {
        if (anim != null) anim.SetTrigger("Knockback");
        yield return new WaitForSeconds(stunTime);

        if (!isDead)
        {
            if (mainRigidbody != null)
            {
                mainRigidbody.linearVelocity = Vector3.zero;
                mainRigidbody.isKinematic = true;
            }
            if (agent != null) agent.enabled = true;
        }
    }

    IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        this.enabled = false;
    }

    public void ApplyKnockback(Vector3 direction, float force)
    {
        pendingKnockbackForce += direction * force;
        hasPendingKnockback = true;
    }

    public void SprayBlood(Vector3 hitPoint, Vector3 hitNormal)
    {
        // Партиклы крови
        if (bloodSprayPrefab != null)
        {
            ParticleSystem blood = Instantiate(bloodSprayPrefab, hitPoint, Quaternion.LookRotation(hitNormal));
            blood.Play();
            Destroy(blood.gameObject, 1.5f);
        }

        // Декаль крови на стене/полу
        if (bloodDecalPrefab != null)
        {
            GameObject decal = Instantiate(bloodDecalPrefab, hitPoint + hitNormal * 0.02f, Quaternion.LookRotation(-hitNormal));
            Destroy(decal, 15f);
        }
    }
}
