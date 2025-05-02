using EventBusEventData;
using UnityEngine;

public class LifestealOnAttack : Perk
{
    public float lifeStealPower;
    [Tooltip("Flat = raw heal, Percentage = enemy max health * (life steal power / 100).")]
    public PassiveValueType valueType;

    HealthComponent health;

    public override void OnEquip(GameObject owner)
    {
        base.OnEquip(owner);

        health = owner.GetComponent<HealthComponent>(); // check if has, throw error if not

        EventBus.Subscribe<PlayerHitEvent>(LifeSteal);
        // Add Summon hit event too
    }

    public override void OnUnEquip(GameObject owner)
    {
        base.OnUnEquip(owner);

        EventBus.Unsubscribe<PlayerHitEvent>(LifeSteal);
    }

    void LifeSteal(PlayerHitEvent e)
    {
        float healAmount = lifeStealPower;

        HealthComponent enemyHealth = e.Enemy.GetComponent<HealthComponent>();
        float enemyMaxHealth = enemyHealth.GetMaxHealth();

        if (valueType == PassiveValueType.Percentage)
        {
            healAmount = enemyMaxHealth * (lifeStealPower / 100f);
        }

        health.Heal(healAmount);
    }
}
