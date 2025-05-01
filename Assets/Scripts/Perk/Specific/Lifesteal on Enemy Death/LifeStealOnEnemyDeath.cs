using EventBusEventData;
using UnityEngine;

public class LifeStealOnEnemyDeath : Perk
{
    public float lifeStealPower;
    [Tooltip("Flat = raw heal, Percentage = enemy max health * (life steal power / 100).")]
    public PassiveValueType valueType;

    HealthComponent health;

    public override void OnEquip(GameObject owner)
    {
        base.OnEquip(owner);

        health = owner.GetComponent<HealthComponent>(); // check if has, throw error if not

        EventBus.Subscribe<EnemyDeathEvent>(LifeSteal);
    }

    public override void OnUnEquip(GameObject owner)
    {
        base.OnUnEquip(owner);

        EventBus.Unsubscribe<EnemyDeathEvent>(LifeSteal);
    }

    void LifeSteal(EnemyDeathEvent e)
    {
        if (valueType == PassiveValueType.Flat)
        {
            health.Heal(lifeStealPower);
            return;
        }

        HealthComponent enemyHealth = e.Enemy.GetComponent<HealthComponent>();

        float enemyMaxHealth = enemyHealth.GetMaxHealth();

        health.Heal(enemyMaxHealth * (lifeStealPower / 100f));
    }
}
