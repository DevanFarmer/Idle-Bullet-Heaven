using EventBusEventData;
using UnityEngine;

public class SpawnMinionOnNearbyEnemyDeath : Perk
{
    [Header("Spawn Requirements")]
    public float requiredDistance;
    [Range(0f, 1f)]
    public float spawnChance;

    [Header("Minion Setup")]
    public GameObject minionPrefab;
    public float minionDuration;

    Transform playerTransform;

    public override void OnEquip(GameObject owner)
    {
        base.OnEquip(owner);

        playerTransform = owner.transform;

        EventBus.Subscribe<EnemyDeathEvent>(OnEnemyDeath);
    }

    public override void OnUnEquip(GameObject owner)
    {
        base.OnUnEquip(owner);

        EventBus.Unsubscribe<EnemyDeathEvent>(OnEnemyDeath);
    }

    void OnEnemyDeath(EnemyDeathEvent e)
    {
        if (Vector3.Distance(playerTransform.position, e.Enemy.transform.position) > requiredDistance) return;

        if (Random.Range(0f, 1f) > spawnChance) return;

        GameObject minion = Instantiate(minionPrefab, e.Enemy.transform.position, Quaternion.identity);

        // make minion only last few seconds
    }
}
