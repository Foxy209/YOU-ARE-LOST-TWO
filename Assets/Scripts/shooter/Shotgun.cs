using UnityEngine;

public class Shotgun : MonoBehaviour
{
    [Header("Параметры выстрела")]
    [SerializeField] private float range = 25f;
    [SerializeField] private int pellets = 8;
    [SerializeField] private float spreadAngle = 4f;
    [SerializeField] private LayerMask hitMask;

    [Header("Зоны урона")]
    [SerializeField] private float closeRange = 5f;
    [SerializeField] private float midRange = 12f;
    [SerializeField] private float damageClose = 100f;
    [SerializeField] private float damageMid = 50f;
    [SerializeField] private float damageFar = 20f;

    [Header("Отбрасывание")]
    [SerializeField] private float knockbackClose = 25f;
    [SerializeField] private float knockbackMid = 12f;
    [SerializeField] private float knockbackFar = 4f;

    [Header("Эффекты")]
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private AudioSource shotSound;

    [Header("Система патронов")]
    [SerializeField] private ShotgunAmmo ammoSystem;

    [Header("Анимация оружия")]
    [SerializeField] private WeaponAnimator weaponAnimator;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (ammoSystem != null && ammoSystem.TryShoot())
            {
                Shoot();
            }
        }
        
    }

    void Shoot()
    {
        if (muzzleFlash != null) muzzleFlash.Play();
        if (shotSound != null) shotSound.Play();
        
        if (weaponAnimator != null) 
            weaponAnimator.PlayShotEffect();

       
        if (muzzleFlash != null) muzzleFlash.Play();
        if (shotSound != null) shotSound.Play();

        
        for (int i = 0; i < pellets; i++)
        {
            Vector3 dir = GetSpreadDirection();
            RaycastHit hit;

            if (Physics.Raycast(transform.position, dir, out hit, range, hitMask))
            {
                float dist = Vector3.Distance(transform.position, hit.point);

                float damage = dist <= closeRange ? damageClose
                             : dist <= midRange ? damageMid
                             : damageFar;

                float knockback = dist <= closeRange ? knockbackClose
                                : dist <= midRange ? knockbackMid
                                : knockbackFar;

                Vector3 knockDir = (hit.collider.transform.position - transform.position).normalized;

                if (hit.collider.TryGetComponent<IDamageable>(out var dmg))
                    dmg.TakeDamage(damage);

                if (hit.collider.TryGetComponent<IKnockbackable>(out var kb))
                    kb.ApplyKnockback(knockDir, knockback);

                if (hit.collider.TryGetComponent<ISprayBlood>(out var blood))
                    blood.SprayBlood(hit.point, hit.normal);

                Debug.DrawLine(transform.position, hit.point, Color.red, 0.1f);
            }
        }
        
    }

    Vector3 GetSpreadDirection()
    {
        Quaternion spread = Quaternion.Euler(
            Random.Range(-spreadAngle, spreadAngle),
            Random.Range(-spreadAngle, spreadAngle),
            0
        );
        return spread * transform.forward;
    }
}
