using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Levelup", menuName = "Stats/Level Up")]
public class LevelUpStats : ScriptableObject
{
    public List<StatModifier> LevelUpStatModifiers = new List<StatModifier>();
}
