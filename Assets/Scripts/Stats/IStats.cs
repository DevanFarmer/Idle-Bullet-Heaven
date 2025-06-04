using System.Collections.Generic;

public interface IStats
{
    public void InitializeCharacterStats(CharacterStats baseStats);

    public void ApplyModifier(StatModifier statModifier);

    public float GetCalculatedStat(StatType statType);

    public void LevelUp(List<StatModifier> levelUpStats);
}
