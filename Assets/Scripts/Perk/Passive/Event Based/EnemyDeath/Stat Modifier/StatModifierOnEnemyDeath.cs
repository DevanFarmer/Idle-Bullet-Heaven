using EventBusEventData;
using System.Collections.Generic;
using UnityEngine;

public class StatModifierOnEnemyDeath : Perk
{
    // Override onEquip, don't add stat modifiers
    // Only add them on EnemyDeathEvent
    // Have a stat cap
    [Header("Increase Requirement")]
    public int increaseDeathRequirement;
    int totalDeaths;

    [Header("Cap Increaces")]
    public int maxIncreases;
    int totalIncreases;

    // Optional: Reset on PlayerHitEvent
    [Header("Reset on Player Hit")]
    [SerializeField] bool onPlayerHit;
    private bool subscribedToPlayerHit;

    public override void OnEquip(GameObject owner)
    {
        totalIncreases = 0;
        totalDeaths = 0;

        EventBus.Subscribe<PlayerHitEvent>(OnEnemyDeath);
        if (onPlayerHit)
        {
            EventBus.Subscribe<PlayerDamagedEvent>(OnPlayerHit);
            subscribedToPlayerHit = true;
        }
    }

    public override void OnUnEquip(GameObject owner)
    {
        EventBus.Unsubscribe<PlayerHitEvent>(OnEnemyDeath);
        if (subscribedToPlayerHit)
        {
            EventBus.Unsubscribe<PlayerDamagedEvent>(OnPlayerHit);
            subscribedToPlayerHit = false;
        }
    }

    void OnEnemyDeath(PlayerHitEvent _event)
    {
        if (totalIncreases > maxIncreases) return;
        if (totalDeaths < increaseDeathRequirement) return;

        ApplyStatModifiers();

        totalDeaths = 0;
    }

    void OnPlayerHit(PlayerDamagedEvent _event)
    {
        for (int i = 0; i <= totalIncreases; i++)
        {
            RemoveStatModifiers();
        }

        totalDeaths = 0;
        totalIncreases = 0;
    }
}
