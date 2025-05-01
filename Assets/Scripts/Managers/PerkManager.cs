using System.Collections.Generic;
using UnityEngine;

public class PerkManager : MonoBehaviour
{
    [SerializeField] List<Perk> perks = new();

    public void EquipPerk(Perk perk)
    {
        perk.OnEquip(this.gameObject);
        perks.Add(perk);
    }

    public void UnEquipPerk(Perk perk)
    {
        perk.OnUnEquip(this.gameObject);
        perks.Remove(perk);
    }

    private void Update()
    {
        foreach (Perk perk in perks)
        {
            if (perk.HasActiveLogic()) perk.OnUpdate(this.gameObject);
        }
    }
}
