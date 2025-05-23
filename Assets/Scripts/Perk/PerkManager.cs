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

    public void GainPerk(Perk perk)
    {
        Perk runtimePerk = Instantiate(perk);
        perkList.Add(runtimePerk);
        runtimePerk.OnEquip(gameObject);
    }

    public void RemovePerk(Perk perk)
    {
        perk.OnUnEquip(gameObject);
        perkList.Remove(perk);
    }
}
