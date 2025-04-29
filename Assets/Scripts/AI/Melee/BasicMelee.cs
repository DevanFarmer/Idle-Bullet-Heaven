using UnityEngine;

[CreateAssetMenu(fileName = "New Basic Melee", menuName = "Attack/Melee/Basic Melee")]
public class BasicMelee : BaseAttack
{
    public override void Attack()
    {
        Collider2D[] hits = GetHitsInRange();

        foreach (Collider2D hit in hits)
        {
            HealthComponent health = hit.GetComponent<HealthComponent>();
            if (health == null) continue;

            health.TakeDamage(attackPower);
        }
    }
}
