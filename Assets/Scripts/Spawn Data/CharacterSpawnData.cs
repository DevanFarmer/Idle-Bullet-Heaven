using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawnData : BaseSpawnData
{
    public string characterName;
    // if dont want to play animation and just display image of character have a image variable here
    public RuntimeAnimatorController animatorController; // will change to clip when i make universal controller then set only clip in there to this clip
    public CharacterStats characterStats;
    public List<Perk> basePerks = new(); // not 100% on this, good for like player but not sure when minions or enemies are going to use

    public override GameObject Spawn(Vector3 spawnPos)
    {
        GameObject spawnObject = base.Spawn(spawnPos);

        SetStats(spawnObject);
        SetPerks(spawnObject);
        SetHealthComponent(spawnObject);

        SetAnimator(spawnObject);

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

    // now returns IHealth(or null) for later use like for AddOnHitActions and using after overriding
    protected virtual IHealth SetHealthComponent(GameObject spawnObject)
    {
        // probably a better way to handle this
        IStats statManager = spawnObject.GetComponent<IStats>();
        // in this case, why doesn't spawnObject have a stat manager?
        if (statManager == null) { Debug.LogWarning($"{spawnObject.name} has no IStats component. Health component not set!"); return null; } // need stat manager

        IHealth health = spawnObject.GetComponent<IHealth>();
        health.SetStatManager(statManager);

        return health;
    }

    // dont need to add to base spawn since they are empty
    protected virtual void AddOnHitActions(GameObject spawnObject, IHealth health) { }

    protected virtual void AddOnDeathActions(GameObject spawnObject, IHealth health) { }

    protected virtual void SetCharacterOnHitEvent(float damage, Transform spawnObject) { }

    // for hitevents set in overridden sethealth methods

    void SetAnimator(GameObject spawnObject)
    {
        // when only storing clip, the universal controller will be gotten through code and added to animator if don't have
        Animator animator = spawnObject.GetComponent<Animator>();
        if (animator == null) { /* got tired of sending warnings */ animator = spawnObject.AddComponent<Animator>(); }

        animator.runtimeAnimatorController = animatorController; // not sure if this work ngl
    }

    // configure animator
}
