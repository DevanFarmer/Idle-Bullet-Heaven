using System.Collections.Generic;
using UnityEngine;

public class PerkManager : MonoBehaviour
{
    List<Perk> perkList;

    void Start()
    {
        foreach (var perk in perkList)
        {
            perk.OnEquip(gameObject);
        }
    }

    void Update()
    {
        foreach(var perk in perkList)
        {
            // check if hasActiveLogic? how performance changing is it?
            perk.OnUpdate(gameObject);
        }
    }

    public void GainPerk(Perk perk)
    {
        perkList.Add(perk);
        perk.OnEquip(gameObject);
    }

    public void RemovePerk(Perk perk)
    {
        perk.OnUnEquip(gameObject);
        perkList.Remove(perk);
    }
}
