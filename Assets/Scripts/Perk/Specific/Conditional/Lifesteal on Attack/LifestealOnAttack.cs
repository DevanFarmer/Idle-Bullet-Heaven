using EventBusEventData;
using UnityEngine;

public class LifestealOnAttack : Perk
{
    public float lifeStealPower;
    [Tooltip("Flat = raw heal, Percentage = damage dealt * (life steal power / 100).")]
    public ModifierType valueType;

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

        float damageDealt = e.Damage;

        if (valueType == ModifierType.Percentage)
        {
            healAmount = damageDealt * (lifeStealPower / 100f);
        }

        health.Heal(healAmount);
    }
}
