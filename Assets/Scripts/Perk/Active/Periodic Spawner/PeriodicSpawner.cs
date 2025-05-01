using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Periodic Spawner", menuName = "Perk/Active/Periodic Spawner")]
public class PeriodicSpawner : Perk
{
    public GameObject spawnPrefab;
    
    public float minSpawnTime;
    public float maxSpawnTime;
    private float lastSpawnTime;
    private float spawnTime;

    public float minSpawnRange;
    public float maxSpawnRange;

    public override void OnEquip(GameObject owner)
    {
        base.OnEquip(owner);

        lastSpawnTime = Time.time;
        SetSpawnTime();

        hasActiveLogic = true;
    }

    public override void OnUpdate(GameObject owner)
    {
        if (lastSpawnTime + spawnTime <= Time.time) // can add to spawner utilty
        {
            HandleSpawns(owner.transform.position);
            SetSpawnTime();
        }
    }

    void SetSpawnTime() // can add to spawner utilty
    {
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    void HandleSpawns(Vector3 centerPoint) // can add to spawner utilty
    {
        float range = Random.Range(minSpawnRange, maxSpawnRange);

        // Generate a random angle between 0 and 360 degrees
        float angle = Random.Range(0f, 2f * Mathf.PI);

        // Generate a random radius (square root ensures uniform distribution)
        float radius = Mathf.Sqrt(Random.Range(0.2f, 1f)) * range;

        // Determine position using polar coordinates
        float x = centerPoint.x + Mathf.Cos(angle) * radius;
        float y = centerPoint.y + Mathf.Sin(angle) * radius;
        Vector2 spawnPosition = new Vector2(x, y);

        // Instantiate the object at the calculated position
        GameObject mine = Instantiate(spawnPrefab, spawnPosition, Quaternion.identity);
    }
}
