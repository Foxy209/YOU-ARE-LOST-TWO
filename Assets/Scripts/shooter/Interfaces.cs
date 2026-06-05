using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float amount);
}

public interface IKnockbackable
{
    void ApplyKnockback(Vector3 direction, float force);
}

public interface ISprayBlood
{
    void SprayBlood(Vector3 hitPoint, Vector3 hitNormal);
}
