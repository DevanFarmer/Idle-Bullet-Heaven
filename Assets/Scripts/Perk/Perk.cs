using System.Collections.Generic;
using UnityEngine;

public abstract class Perk : ScriptableObject
{
    [Header("General")]
    public string perkName;
    public string description;

    [Header("Passive Info")]
    public List<StatModifier> statModifiers = new List<StatModifier>();
    public bool hasActiveLogic;

    // Pass owner for any logic that might need it
    public virtual void OnEquip(GameObject owner)
    {
        foreach (StatModifier modifier in statModifiers)
        {
            StatManager.Instance.ApplyModifier(modifier);
        }
    }

    public abstract void OnUpdate(GameObject owner);

    public virtual void OnUnEquip(GameObject owner)
    {
        foreach (StatModifier modifier in statModifiers)
        {
            StatModifier newModifier = modifier;
            newModifier.value *= -1;
            StatManager.Instance.ApplyModifier(newModifier);
        }
    }

    // Add self as a listener for events like enemy death, exp gained, hit, healed etc.
}
