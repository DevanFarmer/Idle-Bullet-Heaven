using EventBusEventData;
using UnityEngine;

public class InvincibilityHitsOnLowHealth : Perk
{
    [Range(0f, 1f)]
    public float healthThreshold;

    public int invinciblityHits;

    bool hasGainedInvincibilityHits;

    HealthComponent health;

    public override void OnEquip(GameObject owner)
    {
        base.OnEquip(owner);

        health = owner.GetComponent<HealthComponent>();

        hasGainedInvincibilityHits = false;

        EventBus.Subscribe<PlayerHitEvent>(HandleInvincibilityHitsOnLowHealth);
        EventBus.Subscribe<PlayerHealEvent>(HandleReset);
    }

    public override void OnUnEquip(GameObject owner)
    {
        base.OnUnEquip(owner);

        EventBus.Unsubscribe<PlayerHitEvent>(HandleInvincibilityHitsOnLowHealth);
        EventBus.Unsubscribe<PlayerHealEvent>(HandleReset);
    }

    void HandleInvincibilityHitsOnLowHealth(PlayerHitEvent e)
    {
        if (health.GetCurrentHealth() > (health.GetMaxHealth() * healthThreshold)) return;
        if (hasGainedInvincibilityHits) return;
        if (statManager.GetCalculatedStat(StatType.InvincibilityHits) >= invinciblityHits)
        {
            // "Active" perk even if they already have more or the same invincibility hits
            hasGainedInvincibilityHits = true;
            return;
        }

        // need to use constructor since need to do the calculation here
        statManager.ApplyModifier(
            new StatModifier(StatType.InvincibilityHits,
            statManager.GetCalculatedStat(StatType.InvincibilityHits) - invinciblityHits, // dont add more than woudl give
            PassiveValueType.Flat));

        hasGainedInvincibilityHits = true;
    }

    void HandleReset(PlayerHealEvent e)
    {
        // only reset if healed past threshold
        if (health.GetCurrentHealth() <= (health.GetMaxHealth() * healthThreshold)) return;
        hasGainedInvincibilityHits = false;
    }
}
