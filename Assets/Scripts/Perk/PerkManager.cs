using System.Collections.Generic;
using UnityEngine;

public class PerkManager : MonoBehaviour
{
    [SerializeField] List<Perk> perkList;

    void Start()
    {
        // Would only need this if the scene loads with characters, but since everything is spawned in this is unnecessary
        //foreach (var perk in perkList)
        //{
        //    perk.OnEquip(gameObject);
        //}
    }

    void Update()
    {
        foreach(var perk in perkList)
        {
            // check if hasActiveLogic? how performance changing is it?
            perk.OnUpdate(gameObject);
        }
    }

    public void GainPerk(Perk perk, bool upgrade = true)
    {
        Perk runtimePerk;
        if (HasPerk(perk.perkName) && upgrade && perk.upgrade != null)
        {
            runtimePerk = GetPerkByName(perk.name);
            runtimePerk.UpgradePerk();
            return;
        }

        runtimePerk = Instantiate(perk);
        runtimePerk.name = perk.name; // removes the added "(Clone)" suffix

        perkList.Add(runtimePerk);
        runtimePerk.OnEquip(gameObject);
    }

    public void RemovePerk(Perk perk) // might need to do different check since instance might not be the same as parameter perk
    {
        Perk runtimePerk = GetPerkByName(perk.name);
        if (runtimePerk == null) { Debug.LogWarning($"Perk not found! Could not remove {perk.name} from {gameObject.name}!"); return; }
        runtimePerk.OnUnEquip(gameObject);
        perkList.Remove(runtimePerk);
    }

    public List<Perk> GetPerkList()
    {
        return perkList;
    }

    Perk GetPerkByName(string name)
    {
        foreach (Perk perk in perkList)
        {
            if (perk.name == name)
            {
                return perk;
            }
        }
        return null;
    }

    bool HasPerk(string name)
    {
        foreach (Perk perk in perkList)
        {
            if (perk.name == name)
            {
                return true;
            }
        }
        return false;
    }
}
