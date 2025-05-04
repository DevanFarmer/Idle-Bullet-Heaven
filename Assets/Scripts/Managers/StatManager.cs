using UnityEngine;

public class StatManager : MonoBehaviour
{
    // Make this a base class or make stat managers composite components
    
    // remeber to do null checks!

    private void Awake()
    {
        SetBaseStats();
    }

    #region Stats
    #region Structs
    // if i want to separate stats so each character only has the stats they use then would needa use classes instead
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

    Stats baseStats = new Stats();
    Stats flatBonusStats = new Stats();
    Stats percentageBonusStats = new Stats();
    #endregion

    #region Setting Base Stats
    [SerializeField] CharacterStats characterStats;

    void SetBaseStats()
    {
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
            case PassiveValueType.Flat:
                ModifyStat(ref flatBonusStats, modifier.targetStat, modifier.value);
                break;
            case PassiveValueType.Percentage:
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

        Debug.Log("StatType was not found for CalculatedStat");
        return -1;
    }

    float CalculateStat(float baseStat, float flat, float percentage)
    {
        return (baseStat + flat) * percentage;
    }

    // GetBase, GetFlat, GetPercentage
    #endregion
    #endregion

    #region Level Up
    public void LevelUp()
    {
        Debug.Log("Player leveled up!");
    }
    #endregion
}
