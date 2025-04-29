using UnityEngine;

public class StatManager : MonoBehaviour
{
    #region Singleton
    private static StatManager instance = null;

    public static StatManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }
    #endregion

    #region Stats
    #region Structs
    struct Stats
    {
        public float Health;
        public float AttackPower;
        public float AttackSpeed;
        // Add as needed
    }

    Stats baseStats = new Stats();
    Stats flatBonusStats = new Stats();
    Stats percentageBonusStats = new Stats();
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
