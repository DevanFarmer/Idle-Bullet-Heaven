using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour, IStats
{
    //[SerializeField] CharacterStats baseCharacterStats;
    [SerializeField] List<StatEntry> characterStats; // can use dictionary for faster lookups but then cannot see in inspector(maybe switch when add stat screen)

    // best to run by what ever script spawns the character, also to set basestats then initialize, duh!
    public void InitializeCharacterStats(CharacterStats baseStats)
    {
        characterStats = new List<StatEntry>();
        List<StatType> addedStats = new List<StatType>(); // for checking if stat was already added
        foreach (StatEntry stat in baseStats.characterStats)
        {
            if (addedStats.Contains(stat.StatType))
            {
                Debug.Log($"Duplicate stat \"{stat.StatType}\" in {baseStats.name}! Entry was ignored!");
            }
            else characterStats.Add(new StatEntry(stat.StatType, stat.BaseValue, stat.FlatBonusValue, stat.PercentBonusValue));
        }
    }

    public void ApplyModifier(StatModifier statModifier)
    {
        int targetStatIndex = HasStatType(statModifier.targetStat);
        if (targetStatIndex == -1) { Debug.LogWarning($"{gameObject.name} has no {statModifier.targetStat}"); return; } // send debug warning log

        if (statModifier.bonusType == ModifierType.Flat) 
        {
            characterStats[targetStatIndex].ModifyFlatBonus(statModifier.value);
        } 
        else if (statModifier.bonusType == ModifierType.Percentage)
        {
            characterStats[targetStatIndex].ModifyPercentBonus(statModifier.value);
        }
    }

    // returns zero if no stat of given type was found, maybe should return something else instead?
    public float GetCalculatedStat(StatType statType)
    {
        // if (HasStatType(statType) == false) return 0; // send debug warning log
        float value = 0;
        foreach (StatEntry stat in characterStats)
        {
            if (stat.StatType == statType) value = stat.GetCalculatedValue();
        }
        return value;
    }

    // could return an int being the stat index in list and return -1 to show it does not have the stat
    // will not be needed when using dictionary
    int HasStatType(StatType statType)
    {
        for (int i = 0; i < characterStats.Count; i++)
        {
            if (characterStats[i].StatType == statType) return i;
        }
        return -1;
    }

    public void LevelUp(List<StatModifier> levelUpStats)
    {
        foreach (StatModifier statModifier in levelUpStats)
        {
            ApplyModifier(statModifier);
        }
    }
}
