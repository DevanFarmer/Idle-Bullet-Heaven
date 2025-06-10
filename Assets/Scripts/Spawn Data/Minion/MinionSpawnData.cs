using EventBusEventData;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MinionSpawnData", menuName = "Spawn Data/Minion")]
public class MinionSpawnData : NPCSpawnData // make a base spawn data so with name, prefab etc and us as when handling in spawning managers
{
    public float baseSpawnCooldown; // should rather be a stat honestly, since can be modified, made into base cooldown instead
    public List<Weapon> baseWeapons = new(); // i like the idea of minions being able to get weapons like the player, can just be ignored if not good idea

    public override GameObject Spawn(Vector3 spawnPos)
    {
        GameObject minion = base.Spawn(spawnPos);

        IHealth health = minion.GetComponent<IHealth>();

        AddOnHitActions(minion, health);
        AddOnDeathActions(minion, health);

        return minion;
    }

    protected override void AddOnHitActions(GameObject spawnObject, IHealth health)
    {
        health.AddOnHitAction(SetCharacterOnHitEvent);
    }

    protected override void AddOnDeathActions(GameObject spawnObject, IHealth health)
    {
        health.AddOnDeathAction(() => EventBus.Publish(new MinionDeathEvent()));
        health.AddOnDeathAction(() => Destroy(spawnObject));
    }

    protected override void SetCharacterOnHitEvent(float damage, Transform spawnObject)
    {
        EventBus.Publish(new MinionHitEvent(damage));
    }

    protected override IHealth SetHealthComponent(GameObject spawnObject)
    {
        IHealth health = base.SetHealthComponent(spawnObject);
        health.UpdateMaxHealth();
        return health;
    }

    void SetWeapons(GameObject minion)
    {
        WeaponManager weaponManager = minion.GetComponent<WeaponManager>();
        if (weaponManager == null) { /* ignore sending warning since might not use this */ weaponManager = minion.AddComponent<WeaponManager>(); }

        // problem with obtaining on start since if i now add them here they will be added twice
        // ignore for now
    }

    // + decay manager - nevermind I forgot manager already handles that
    // * weapon manager
}
