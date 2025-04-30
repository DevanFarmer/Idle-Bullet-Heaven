using System.Collections.Generic;
using System.Linq;
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

    [Header("Passive Info")]
    public List<StatModifier> statModifiers = new List<StatModifier>();
    protected bool hasActiveLogic = true;

    // Pass owner for any logic that might need it
    public virtual void OnEquip(GameObject owner)
    {
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

    protected void ApplyStatModifiers()
    {
        foreach (StatModifier modifier in statModifiers)
        {
            StatManager.Instance.ApplyModifier(modifier);
        }
    }

    protected void RemoveStatModifiers()
    {
        foreach (StatModifier modifier in statModifiers)
        {
            StatModifier newModifier = modifier;
            newModifier.value *= -1;
            StatManager.Instance.ApplyModifier(newModifier);
        }
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
            if (mod.bonusType == PassiveValueType.Percentage) effectDescription += "%"; // shows if percentage
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
