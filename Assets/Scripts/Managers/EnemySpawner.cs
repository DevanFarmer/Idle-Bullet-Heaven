using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<Enemy> enemies = new List<Enemy>();

    [SerializeField] float spawnOffset;
    [SerializeField] int maxSingleSpawn;
    [SerializeField] float minSpawnTime;
    [SerializeField] float maxSpawnTime;

    private float lastSpawnTime;
    private float spawnTime;

    private void Start()
    {
        lastSpawnTime = Time.time;
        SetSpawnTime();
    }

    private void Update()
    {
        if (lastSpawnTime + spawnTime <= Time.time) // can add to spawner utilty
        {
            HandleEnemySpawns();
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

    void SpawnEnemy(Enemy enemyCharacter)
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
        Camera cam = Camera.main;

        // Get camera bounds(right and bottom of camera)
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        // Set spawn to top and left and randomly switch to bottom or right sides
        float xPos = -spawnOffset, yPos = -spawnOffset;
        if (Random.Range(0, 2) == 1) xPos = camWidth + spawnOffset;
        if (Random.Range(0, 2) == 1) yPos = camHeight + spawnOffset;

        return new Vector3(xPos, yPos);
    }
}
