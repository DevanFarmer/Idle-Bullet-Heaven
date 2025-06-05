using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawnData : BaseSpawnData
{
    public CharacterStats characterStats;
    public List<Perk> basePerks = new(); // not 100% on this, good for like player but not sure when minions or enemies are going to use

    public override GameObject Spawn(Vector3 spawnPos)
    {
        GameObject spawnObject = base.Spawn(spawnPos);

        SetStats(spawnObject);
        SetPerks(spawnObject);
        SetHealthComponent(spawnObject);

        return spawnObject;
    }

    void SetStats(GameObject spawnObject)
    {
        IStats statManager = spawnObject.GetComponent<IStats>();
        if (statManager == null) { Debug.LogWarning($"{spawnObject.name} has no IStats component. StatManager added!"); 
            statManager = spawnObject.AddComponent<StatManager>(); }

        statManager.InitializeCharacterStats(characterStats);
    }

    void SetPerks(GameObject spawnObject)
    {
        IPerkManager perkManager = spawnObject.GetComponent<IPerkManager>();
        if (perkManager == null) { Debug.LogWarning($"{spawnObject.name} has no IPerkManager componenet. PerkManager added!"); 
            perkManager = spawnObject.AddComponent<PerkManager>(); }

        foreach (Perk perk in basePerks)
        {
            perkManager.GainPerk(perk);
        }
    }

    protected virtual void SetHealthComponent(GameObject spawnObject)
    {
        // probably a better way to handle this
        IStats statManager = spawnObject.GetComponent<IStats>();
        // in this case, why doesn't spawnObject have a stat manager?
        if (statManager == null) { Debug.LogWarning($"{spawnObject.name} has no IStats component. Health component not set!"); return; }

        IHealth health = spawnObject.GetComponent<IHealth>();
        health.SetStatManager(statManager);
    }

    // dont need to add to base spawn since they are empty
    protected virtual void AddOnHitActions(GameObject spawnObject) { }

    protected virtual void AddOnDeathActions(GameObject spawnObject) { }

    protected virtual void SetCharacterOnHitEvent(float damage, Transform spawnObject) { }

    // for hitevents set in overridden sethealth methods
}
