using UnityEngine;

public class StatManager : MonoBehaviour
{
    // Make this a base class or make stat managers composite components
    
    // remeber to do null checks!

    private void Awake()
    {
        if (characterStats != null) SetBaseStats();
    }

    #region Stats
    #region Structs
    // if i want to separate stats so each character only has the stats they use then would needa use classes instead
    [System.Serializable]
    struct Stats
    {
        public float Health;
        public float AttackPower;
        public float AttackSpeed;

        public int InvincibilityHits; // how is health component going to be updated with new amount?
                                      // maybe has an effects class or something that handles things like these

        public float MinionAttackSpeed;
        // Add as needed
    }

    [SerializeField] Stats baseStats = new Stats();
    [SerializeField] Stats flatBonusStats = new Stats();
    [SerializeField] Stats percentageBonusStats = new Stats();
    #endregion

    #region Setting Base Stats
    [SerializeField] CharacterStats characterStats;
    public void SetCharacterStats(CharacterStats characterStats)
    {
        this.characterStats = characterStats;
        SetBaseStats();
    }

    void SetBaseStats()
    {
        if (characterStats == null)
        {
            Debug.LogError($"No character stats set on {gameObject.name}!");
            return;
        }
        baseStats.Health = characterStats.Health;
        baseStats.AttackPower = characterStats.AttackPower;
        baseStats.AttackSpeed = characterStats.AttackSpeed;

        baseStats.InvincibilityHits = characterStats.InvincibilityHits;
    }
    #endregion

    #region Stat Modifying
    public void ApplyModifier(StatModifier modifier)
    {
        switch (modifier.bonusType)
        {
            case ModifierType.Flat:
                ModifyStat(ref flatBonusStats, modifier.targetStat, modifier.value);
                break;
            case ModifierType.Percentage:
                ModifyStat(ref percentageBonusStats, modifier.targetStat, modifier.value);
                break;
        }
    }

    void ModifyStat(ref Stats stats, StatType statType, float value)
    {
        switch (statType)
        {
            case StatType.Health:
                stats.Health += value;
                break;
            case StatType.AttackPower:
                stats.AttackPower += value;
                break;
            case StatType.AttackSpeed:
                stats.AttackSpeed += value;
                break;
            case StatType.InvincibilityHits:
                stats.InvincibilityHits += (int)value;
                break;
            case StatType.MinionAttackSpeed:
                stats.MinionAttackSpeed += value;
                break;
        }
    }
    #endregion

    #region Sending Calculated Stats
    public float GetCalculatedStat(StatType statType)
    {
        switch (statType)
        {
            case StatType.Health:
                return CalculateStat(baseStats.Health, flatBonusStats.Health, percentageBonusStats.Health);
            case StatType.AttackPower:
                return CalculateStat(baseStats.AttackPower, flatBonusStats.AttackPower, percentageBonusStats.AttackPower);
            case StatType.AttackSpeed:
                return CalculateStat(baseStats.AttackSpeed, flatBonusStats.AttackSpeed, percentageBonusStats.AttackSpeed);
            case StatType.InvincibilityHits:
                return baseStats.InvincibilityHits;
        }

        Debug.Log($"StatType was not found for CalculatedStat. StatType: {statType}");
        return -1;
    }

    float CalculateStat(float baseStat, float flat, float percentage)
    {
        float statValue = baseStat + flat;

        // if percentage 0 just return base + flat to stop it from setting stat to 0,
        if (percentage == 0) return statValue;
        // if percentage is negative, while subtract from base + flat
        return statValue + (statValue * (percentage / 100f));
    }

    // Makes GetBase, GetFlat, GetPercentage methods
    #endregion
    #endregion

    #region Level Up
    public void LevelUp(LevelUpStats levelUpStats) // take in a so that stores the upgraded stats
    {
        Debug.Log($"{gameObject.name} leveled up!");
        foreach (StatModifier levelUp in levelUpStats.LevelUpStatModifiers)
        {
            // could make another struct to store level up stats
            ApplyModifier(levelUp);
        }
    }
    #endregion
}
