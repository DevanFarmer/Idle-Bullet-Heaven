using System.Collections.Generic;

public interface IPerkManager
{
    public void GainPerk(Perk perk);
    public void RemovePerk(Perk perk);
    public List<Perk> GetPerkList();
}
