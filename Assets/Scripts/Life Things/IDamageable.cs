using UnityEngine;
public interface IDamageable 
{
    void TakeDamage(float damage, Vector3 particlePosition);
    void Death(Vector3 particlePosition);
    void DestroySelf();

}
