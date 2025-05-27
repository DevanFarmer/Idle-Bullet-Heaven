using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

// stored on spawners and used to initialize statmanager
[CreateAssetMenu(menuName = "Stats/New Character Stats")]
public class CharacterStats : ScriptableObject
{
    [SerializeField, TextArea(1, 15)] string statTypes;
    // could make a baseStats so and store here and loop and add in intialize character stats in stat manager
    public List<StatEntry> characterStats = new();

    // Base Stats that the script will try to always have in characterStats list
    private static readonly HashSet<StatType> baseStatTypes = new HashSet<StatType>()
    {
        StatType.Health,
        StatType.AttackPower,
        StatType.AttackSpeed,
        StatType.CriticalChance,
        StatType.AttackRange,
        StatType.AttackSize,
        StatType.MoveSpeed, // Player can just have for ease of the other two types that need it
        StatType.DetectionRange // same with this
    };

#if UNITY_EDITOR
    void OnValidate()
    {
        ValidateBaseStats();
        UpdateStatTypesVariable();
    }

    void ValidateBaseStats()
    {
        // so it doesnt accidentally modify during play
        if (Application.isPlaying) return;

        bool changed = false;

        // Ensure base StatTypes are present
        foreach (StatType statType in baseStatTypes)
        {
            if (!characterStats.Any(e => e.StatType == statType))
            {
                characterStats.Add(new StatEntry(statType, 0, 0, 0));
                changed = true;
            }
        }

        // Keep list sorted by enum order, could do in own method
        characterStats = characterStats
            .OrderBy(e => baseStatTypes.Contains(e.StatType) ? 0 : 1)
            .ThenBy(e => (int)e.StatType)
            .ToList();

        if (changed)
        {
            // Mark the SO dirty so Unity knows to save the changes
            EditorUtility.SetDirty(this);
        }
    }

    void UpdateStatTypesVariable()
    {
        statTypes = "";
        for (int i = 0; i < characterStats.Count; i++)
        {
            if (statTypes != "") statTypes += "\n"; // Only break line when adding a stat, ignores first
            statTypes += $"{i+1}.\t{characterStats[i].StatType}\nBase Value: {characterStats[i].BaseValue}";
        }
        //foreach (StatEntry stat in characterStats)
        //{
        //    if (statTypes != "") statTypes += "\n"; // Only break line when adding a stat, ignores first
        //    statTypes += stat.StatType;
        //}
    }
#endif
}
