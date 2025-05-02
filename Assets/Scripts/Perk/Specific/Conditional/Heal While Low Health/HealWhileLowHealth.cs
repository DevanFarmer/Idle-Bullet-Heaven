using System.Collections.Generic;
using UnityEngine;

// While health below x% heal x(%) every x seconds
public class HealWhileLowHealth : Perk
{
    [Range(1f, 100f)]
    public float thresfold;

    public float healValue;
    public PassiveValueType healType;

    public float healTick;

    float lastHealTick;

    HealthComponent health;

    public override void OnEquip(GameObject owner)
    {
        base.OnEquip(owner);

        health = owner.GetComponent<HealthComponent>();
        lastHealTick = Time.time;
    }

    public override void OnUpdate(GameObject owner)
    {
        HandlePerkLogic();
    }

    void HandlePerkLogic()
    {
        if (lastHealTick + healTick > Time.time) return;

        if ((health.GetCurrentHealth() / health.GetMaxHealth()) > thresfold) return;

        float healAmount = healValue;
        if (healType == PassiveValueType.Percentage) healAmount = health.GetMaxHealth() * (healValue / 100f);
        health.Heal(healAmount);

        lastHealTick = Time.time;
    }
}
