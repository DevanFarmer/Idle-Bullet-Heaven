public class PerkChoice
{
    public Perk DisplayPerk;
    public Perk BasePerk;

    public PerkChoice(Perk displayPerk, Perk basePerk = null)
    {
        DisplayPerk = displayPerk;
        if (basePerk == null) BasePerk = displayPerk;
        else BasePerk = basePerk;
    }
}
