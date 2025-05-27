[System.Serializable]
public class StatEntry
{
    public StatType StatType;
    public float BaseValue;
    public float FlatBonusValue;
    public float PercentBonusValue;

    public StatEntry(StatType statType, float baseValue, float flatBonusValue, float percentBonusValue)
    {
        StatType = statType;
        BaseValue = baseValue;
        FlatBonusValue = flatBonusValue;
        PercentBonusValue = percentBonusValue;
    }

    public void ModifyFlatBonus(float value)
    {
        FlatBonusValue += value;
    }

    public void ModifyPercentBonus(float value)
    {
        PercentBonusValue += value;
    }

    public float GetCalculatedValue()
    {
        float statValue = BaseValue + FlatBonusValue;

        // if percentage 0 just return base + flat to stop it from setting stat to 0,
        if (PercentBonusValue == 0) return statValue;
        // if percentage is negative, while subtract from base + flat
        return statValue + (statValue * (PercentBonusValue / 100f));
    }
}
