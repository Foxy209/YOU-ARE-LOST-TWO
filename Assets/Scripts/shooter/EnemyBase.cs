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

    [Header("Отбрасывание")]
    [SerializeField] private float stunTime = 0.5f;

    [Header("Компоненты")]
    [SerializeField] private Animator anim;
    [SerializeField] private Collider mainCollider;

    private NavMeshAgent agent;
    private Rigidbody mainRigidbody;
    private List<Rigidbody> ragdollRigidbodies = new List<Rigidbody>();
    private List<Collider> ragdollColliders = new List<Collider>();

    private bool isDead;
    private Vector3 pendingKnockbackForce;
    private bool hasPendingKnockback;
    private bool knockbackAppliedThisFrame;
    private bool ragdollEnabled;

    void Start()
    {
        currentHealth = maxHealth;
        agent = GetComponent<NavMeshAgent>();
        mainRigidbody = GetComponent<Rigidbody>();

        if (anim == null) anim = GetComponent<Animator>();
        if (mainCollider == null) mainCollider = GetComponent<Collider>();

        // Собираем все Rigidbody с костей
        Rigidbody[] allRbs = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in allRbs)
        {
            if (rb != mainRigidbody)
            {
                ragdollRigidbodies.Add(rb);
                rb.isKinematic = true;
            }
        }

        // Собираем все коллайдеры с костей
        Collider[] allCols = GetComponentsInChildren<Collider>();
        foreach (Collider col in allCols)
        {
            if (col != mainCollider)
            {
                ragdollColliders.Add(col);
                col.enabled = false;
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

        if (!isDead)
        {
            // Живой — толкаем основной Rigidbody
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
            // Мёртвый — включаем регдолл и толкаем
            EnableRagdoll();
            StartCoroutine(ApplyForceToRagdollNextFrame(pendingKnockbackForce));
            pendingKnockbackForce = Vector3.zero;
        }
    }

    IEnumerator ApplyForceToRagdollNextFrame(Vector3 force)
    {
        yield return null;

        // Толкаем ВСЕ кости регдолла для надёжности
        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            if (rb != null)
            {
                rb.AddForce(force * 0.3f, ForceMode.Impulse); // делим силу на все кости
            }
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

        if (anim != null) anim.enabled = false;
        if (agent != null) agent.enabled = false;

        // Основной коллайдер НЕ отключаем сразу!
        // Он ещё нужен, пока ragdoll не включится
    }

    void EnableRagdoll()
    {
        if (ragdollEnabled) return;
        ragdollEnabled = true;

        // Отключаем основной коллайдер и Rigidbody
        if (mainCollider != null) mainCollider.enabled = false;
        if (mainRigidbody != null)
        {
            mainRigidbody.isKinematic = true;
            mainRigidbody.detectCollisions = false;
        }

        // Включаем все кости
        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.linearVelocity = Vector3.zero;
            }
        }
        foreach (Collider col in ragdollColliders)
        {
            if (col != null)
            {
                col.enabled = true;
            }
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
        if (bloodSprayPrefab != null)
        {
            ParticleSystem blood = Instantiate(bloodSprayPrefab, hitPoint, Quaternion.LookRotation(hitNormal));
            blood.Play();
            Destroy(blood.gameObject, 1.5f);
        }
    }
}
