using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<EnemySpawnData> enemies = new List<EnemySpawnData>();

    [SerializeField] float spawnOffset;
    [SerializeField] int maxSingleSpawn;
    [SerializeField] float minSpawnTime;
    [SerializeField] float maxSpawnTime;

    private float lastSpawnTime;
    private float spawnTime;

    Camera cam;

    private void Start()
    {
        cam = Camera.main;

        lastSpawnTime = Time.time;
        SetSpawnTime();
    }

    private void Update()
    {
        if (lastSpawnTime + spawnTime <= Time.time) // can add to spawner utilty
        {
            HandleEnemySpawns();
            lastSpawnTime = Time.time;
            SetSpawnTime();
        }
    }

    void SetSpawnTime() // can add to spawner utilty
    {
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    void HandleEnemySpawns() // can add to spawner utilty
    {
        int numberOfSpawns = Random.Range(1, maxSingleSpawn + 1);

        for (int i = 0; i < numberOfSpawns; i++)
        {
            SpawnEnemy(enemies[Random.Range(0, enemies.Count)]);
        }
    }

    void SpawnEnemy(EnemySpawnData enemyCharacter)
    {
        GameObject enemy = Instantiate(enemyCharacter.prefab, GetOffscreenPosition(), Quaternion.identity);

        ExperienceGiver expGiver = enemy.GetComponent<ExperienceGiver>();
        if (expGiver == null) { expGiver = enemy.AddComponent<ExperienceGiver>(); }
        expGiver.SetExpPoints(enemyCharacter.expPoints);

        HealthComponent health = enemy.GetComponent<HealthComponent>();
        if (health == null) { Debug.Log($"No HealthComponent found on {enemy.name}. Empty HealthComponent Added!"); health = enemy.AddComponent<HealthComponent>(); }

        health.onDeath += expGiver.GiveExperience;
        health.onDeath += () => { Destroy(enemy); };
    }

    // add options to only spawn from certain sides
    Vector3 GetOffscreenPosition()
    {
        // Choose main side(top, left, down, right)
        // Set x or y to main side plus offset
        // Get random value from (top to bottom or left to right)

        // Rework this

        // Get corners
        // Bottom-left corner
        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        // Top-left corner
        Vector3 topLeft = cam.ViewportToWorldPoint(new Vector3(0, 1, cam.nearClipPlane));
        // Top-right corner
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
        // Bottom-right corner
        Vector3 bottomRight = cam.ViewportToWorldPoint(new Vector3(1, 0, cam.nearClipPlane));

        // Choose a random side to be the main side
        int randomside = Random.Range(0, 4);

        float xPos = 0, yPos = 0;

        switch (randomside)
        {
            case 0: // top
                yPos = topLeft.y + spawnOffset;
                xPos = Random.Range(topLeft.x, topRight.x);
                break;
            case 1: // left
                xPos = topLeft.x - spawnOffset;
                yPos = Random.Range(topLeft.y, bottomLeft.y);
                break;
            case 2: // right
                xPos = topRight.x + spawnOffset;
                yPos = Random.Range(topRight.y, bottomRight.y);
                break;
            case 3: // bottom
                yPos = bottomLeft.y - spawnOffset;
                xPos = Random.Range(bottomLeft.x, bottomRight.x);
                break;
        }

        return new Vector3(xPos, yPos, 0f);
    }
}
