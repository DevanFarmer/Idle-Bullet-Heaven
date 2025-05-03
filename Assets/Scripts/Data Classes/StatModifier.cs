[System.Serializable]
public class StatModifier
{
    public StatType targetStat;
    public float value;
    public PassiveValueType bonusType;

    public StatModifier(StatType targetStat, float value, PassiveValueType bonusType)
    {
        this.targetStat = targetStat;
        this.value = value;
        this.bonusType = bonusType;
    }
}
