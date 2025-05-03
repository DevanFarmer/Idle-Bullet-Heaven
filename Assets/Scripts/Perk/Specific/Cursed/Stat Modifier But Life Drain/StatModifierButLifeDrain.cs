using UnityEngine;

public class StatModifierButLifeDrain : Perk
{
    public float healthDrain;
    [Tooltip("Flat = raw damage; Percentage = percentage of current max health")]
    public PassiveValueType drainType;

    public float drainTick;
    float lastDrainTick;

    HealthComponent health;

    public override void OnEquip(GameObject owner)
    {
        base.OnEquip(owner);

        health = owner.GetComponent<HealthComponent>();

        lastDrainTick = Time.time;
    }

    public override void OnUpdate(GameObject owner)
    {
        if (lastDrainTick + drainTick <= Time.time)
        {
            DrainHealth();

            lastDrainTick = Time.time;
        }
    }

    void DrainHealth()
    {
        float damageAmount = healthDrain;

        if (drainType == PassiveValueType.Percentage)
        {
            damageAmount = health.GetMaxHealth() * (healthDrain / 100f);
        }

        // While trigger player hit though
        health.TakeDamage(damageAmount, false);
    }
}
