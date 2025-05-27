using UnityEngine;

[CreateAssetMenu(fileName = "New Basic Melee", menuName = "Attack/Melee/Basic Melee")]
public class BasicMelee : BaseAttack
{
    public override void Attack(Transform character, Transform target, IStats statManager)
    {
        HealthComponent health = target.GetComponent<HealthComponent>();
        if (health == null) return;

        health.TakeDamage(GetDamage(statManager));
    }
}
