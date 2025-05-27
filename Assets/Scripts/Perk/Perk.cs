using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Perk : ScriptableObject
{
    [Header("General")]
    public string perkName;
    [TextArea(1, 10)]
    public string description;
    [TextArea(1, 10)]
    public string formattedDescription;
    public string effectDescription;

    // All weapons now inherit this?
    // Rather Create a Passive child that has this, if weapon needs just special case for it then
    [Header("Passive Info")]
    public List<StatModifier> statModifiers = new List<StatModifier>();
    protected bool hasActiveLogic = true;
    
    protected IStats statManager;

    // Pass owner for any logic that might need it
    public virtual void OnEquip(GameObject owner)
    {
        statManager = owner.GetComponent<IStats>(); // Can have an initializer method that always runs but cannot be overridden

        ApplyStatModifiers();
    }

    public virtual void OnUpdate(GameObject owner)
    {
        // Could make abstract but for few cases might not have update needed so saves having to always override
    }

    public virtual void OnUnEquip(GameObject owner)
    {
        RemoveStatModifiers();
    }

    // Add self as a listener for events like enemy death, exp gained, hit, healed etc.

    /// <summary>
    /// Adds stat modifications to stat manager.
    /// </summary>
    /// <param name="modifier">Used for perks that want to modify the stats value by a multiple.</param>
    protected void ApplyStatModifiers(float modifier = 1)
    {
        float newValue = 0;
        foreach (StatModifier statModifier in statModifiers)
        {
            newValue = statModifier.value * modifier;
            if (statModifier.bonusType == ModifierType.Percentage) newValue /= 100;
            ApplyModifier(new StatModifier(statModifier.targetStat, 
                                           newValue,
                                           statModifier.bonusType));
        }
    }

    /// <summary>
    /// Removes stat modifications done by the perk.
    /// </summary>
    /// <param name="modifier">Used for perks that want to modify the stats value by a multiple.</param>
    protected void RemoveStatModifiers(float modifier = 1)
    {
        float newValue = 0;
        foreach (StatModifier statModifier in statModifiers)
        {
            newValue = statModifier.value * modifier;
            if (statModifier.bonusType == ModifierType.Percentage) newValue /= 100;
            ApplyModifier(new StatModifier(statModifier.targetStat,
                                           newValue *= -1,
                                           statModifier.bonusType));
        }
    }

    void ApplyModifier(StatModifier modifier)
    {
        statManager.ApplyModifier(modifier);
    }

    public bool HasActiveLogic()
    {
        return hasActiveLogic;
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        UpdateEffectDescriptionFromStats();
        FormatDescription();
    }

    public void UpdateEffectDescriptionFromStats()
    {
        effectDescription = "";
        foreach (StatModifier mod in statModifiers)
        {
            string color = mod.value >= 0 ? "green" : "red";
            string sign = mod.value >= 0 ? "+" : "-";
            if (effectDescription != "") effectDescription += "\n"; // Only break line when adding a stat, ignores first
            effectDescription += $"<color={color}>{sign}{Mathf.Abs(mod.value)}</color>"; // Adds value, adds color
            if (mod.bonusType == ModifierType.Percentage) effectDescription += "%"; // shows if percentage
            effectDescription += $" {mod.targetStat}"; // Adds stat type
        }
    }

    public void FormatDescription()
    {
        // Could add the color by making the list be of string and check value
        List<float> values = new();
        foreach (StatModifier statModifier in statModifiers)
        {
            values.Add(statModifier.value);
        }
        formattedDescription = string.Format(description, values.Cast<object>().ToArray());

        // Example:
        //List<int> statValues = new List<int> { 15, 2 };
        //string format = "Health: {0}, Attack: {1}";

        //string result = string.Format(format, statValues.Cast<object>().ToArray());
        //Output: "Health: 15, Attack: 2"
    }
#endif
}
