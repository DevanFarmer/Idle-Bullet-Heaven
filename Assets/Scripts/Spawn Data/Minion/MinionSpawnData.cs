using EventBusEventData;
using UnityEngine;

[CreateAssetMenu(fileName = "New MinionSpawnData", menuName = "Minion/Spawn Data")]
public class MinionSpawnData : CharacterSpawnData // make a base spawn data so with name, prefab etc and us as when handling in spawning managers
{
    public float spawnCooldown;

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
}
