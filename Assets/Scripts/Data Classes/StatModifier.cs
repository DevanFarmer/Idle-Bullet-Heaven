[System.Serializable]
public class StatModifier
{
    public StatType targetStat;
    public float value;
    public ModifierType bonusType;

    public StatModifier(StatType targetStat, float value, ModifierType bonusType)
    {
        this.targetStat = targetStat;
        this.value = value;
        this.bonusType = bonusType;
    }
}
