using EventBusEventData;
using UnityEngine;

public class StatModifierOnKillStreak : Perk
{
    public int enemiesNeededToTrigger;
    int totalEnemiesKilled;

    int stacks;

    bool firstTrigger;

    public override void OnEquip(GameObject owner)
    {
        totalEnemiesKilled = 0;
        stacks = 0;
        firstTrigger = true;

        EventBus.Subscribe<EnemyDeathEvent>(OnEnemyKilled);
        EventBus.Subscribe<PlayerHitEvent>(OnPlayerHit);
    }

    public override void OnUnEquip(GameObject owner)
    {
        RemoveModifiers();

        EventBus.Unsubscribe<EnemyDeathEvent>(OnEnemyKilled);
        EventBus.Unsubscribe<PlayerHitEvent>(OnPlayerHit);
    }

    void OnEnemyKilled(EnemyDeathEvent e)
    {
        totalEnemiesKilled++;

        if (totalEnemiesKilled % enemiesNeededToTrigger == 0)
        {
            IncreaseStack();
        }
    }

    void OnPlayerHit(PlayerHitEvent e)
    {
        // Reset stats
        RemoveModifiers();

        // only do after since its needed for the modifier
        stacks = 0;
    }

    void IncreaseStack()
    {
        // first go will remove stats that were not added if not for this
        if (!firstTrigger)
        {
            RemoveModifiers(); 
        } 
        else firstTrigger = false;
        
        stacks++;
        AddModifiers();
    }

    // adds modifiers with new stack
    void AddModifiers()
    {
        foreach (StatModifier statModifier in statModifiers)
        {
            ApplyStatModifiers(stacks);
        }
    }

    // removes modifiers from previous stack
    void RemoveModifiers()
    {
        foreach (StatModifier statModifier in statModifiers)
        {
            RemoveStatModifiers(stacks);
        }
    }
}
