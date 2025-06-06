using EventBusEventData;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerSpawnData", menuName = "Spawn Data/Player")]
public class PlayerSpawnData : CharacterSpawnData
{
    // doesn't do much now, but if i want to add stuff will just add here

    public override GameObject Spawn(Vector3 spawnPos)
    {
        GameObject player = base.Spawn(spawnPos);
        
        IHealth health = player.GetComponent<IHealth>();

        AddOnHitActions(player, health);
        AddOnDeathActions(player, health);
        
        return player;
    }

    protected override void AddOnHitActions(GameObject spawnObject, IHealth health)
    {
        health.AddOnHitAction(SetCharacterOnHitEvent);
    }

    protected override void AddOnDeathActions(GameObject spawnObject, IHealth health)
    {
        health.AddOnDeathAction(() => EventBus.Publish(new PlayerDeathEvent()));
        //health.AddOnDeathAction(() => Destroy(spawnObject)); Game Over!
    }

    protected override void SetCharacterOnHitEvent(float damage, Transform spawnObject)
    {
        EventBus.Publish(new PlayerHitEvent(damage));
    }

    protected override IHealth SetHealthComponent(GameObject spawnObject)
    {
        IHealth health = base.SetHealthComponent(spawnObject);
        health.UpdateMaxHealth();
        return health;
    }
}
