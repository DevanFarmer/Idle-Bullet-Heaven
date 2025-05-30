using EventBusEventData;
using UnityEngine;

public class LifeStealOnEnemyDeath : Perk
{
    public float lifeStealPower;
    [Tooltip("Flat = raw heal, Percentage = enemy max health * (life steal power / 100).")]
    public ModifierType valueType;

    HealthComponent health;

    public override void OnEquip(GameObject owner)
    {
        base.OnEquip(owner);

        health = owner.GetComponent<HealthComponent>(); // check if has, throw error if not

        EventBus.Subscribe<EnemyHitEvent>(LifeSteal);
    }

    public override void OnUnEquip(GameObject owner)
    {
        base.OnUnEquip(owner);

        EventBus.Unsubscribe<EnemyHitEvent>(LifeSteal);
    }

    void LifeSteal(EnemyHitEvent e)
    {
        float healAmount = lifeStealPower;

        HealthComponent enemyHealth = e.Enemy.GetComponent<HealthComponent>();
        float enemyMaxHealth = enemyHealth.GetMaxHealth();

        if (valueType == ModifierType.Percentage)
        {
            healAmount = enemyMaxHealth * (lifeStealPower / 100f);
        }

        health.Heal(healAmount);
    }
}
