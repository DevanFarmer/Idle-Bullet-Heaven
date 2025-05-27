using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // could remove local variables and only use wave data

    [SerializeField] List<EnemySpawnData> enemies = new List<EnemySpawnData>();

    [SerializeField] float spawnOffset;
    [Header("Spawn Numbers")]
    [SerializeField] int minSingleSpawn;
    [SerializeField] int maxSingleSpawn;

    [Header("Spawn Times")]
    [SerializeField] float minSpawnTime;
    [SerializeField] float maxSpawnTime;
    private float lastSpawnTime;
    private float spawnTime;

    [Header("Enemy Waves")]
    [SerializeField] List<EnemyWave> enemyWaves = new();
    [SerializeField] float checkDelay;
    [SerializeField] int waveLevel; // will never be max level since starts at 0, this is fine as it will run the max wave
    [SerializeField] int maxWaveLevel;
    GameTimeManager gameTimeManager;


    [Header("Hoardes")] // make scriptable object with these stats
    [SerializeField] HoardData hoardData;
    float lastHoardTime;
    int hoardRoll;

    [Header("VFX")]
    [SerializeField] GameObject damagePopup;

    Camera cam;

    private void Start()
    {
        cam = Camera.main;

        gameTimeManager = GameTimeManager.Instance;

        waveLevel = -1; // set to 0 since will increase eave level on initial levelup
        maxWaveLevel = enemyWaves.Count;

        lastSpawnTime = Time.time;
        lastHoardTime = Time.time;
        hoardRoll = 1000;

        LevelUpEnemies(enemyWaves[0]);

        StartCoroutine(HandleWaveChecks());
        
        SetSpawnTime();
    }

    private void Update()
    {
        if (lastSpawnTime + spawnTime <= Time.time) // can add to spawner utilty
        {
            HandleEnemySpawns(minSingleSpawn, maxSingleSpawn);
            lastSpawnTime = Time.time;
            SetSpawnTime();
        }

        if (hoardData.canSpawnHoard)
        {
            if (lastHoardTime + hoardData.hoardCheckTime <= Time.time)
            {
                hoardRoll = Random.Range(0, 101);
                if (hoardRoll <= hoardData.hoardChance)
                {
                    HandleEnemySpawns(hoardData.minHoardSpawns, hoardData.maxHoardSpawns); // can make a method to bunch hoard
                }
                lastHoardTime = Time.time;
            }
        }
    }

    void SetSpawnTime() // can add to spawner utilty
    {
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    void HandleEnemySpawns(int minSpawns, int maxSpawns) // can add to spawner utilty
    {
        int numberOfSpawns = Random.Range(minSpawns, maxSpawns + 1);

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

        IStats statManager = enemy.GetComponent<IStats>();
        if (statManager == null) { statManager = enemy.AddComponent<StatManager>(); }
        statManager.InitializeCharacterStats(enemyCharacter.stats);

        HealthComponent health = enemy.GetComponent<HealthComponent>();
        health.SetStatManager(statManager);
        if (health == null) { Debug.Log($"No HealthComponent found on {enemy.name}. Empty HealthComponent Added!"); health = enemy.AddComponent<HealthComponent>(); }

        health.onDeath += expGiver.GiveExperience;
        health.onDeath += () => { Destroy(enemy); };

        health.onHit += (damage, hitGameObject) => {
            GameObject popup = Instantiate(damagePopup, hitGameObject.position, Quaternion.identity);
            popup.GetComponentInChildren<TextMeshPro>().text = damage.ToString();
        };

        // target component handles setting target
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

    // use a coroutine with a delay to stop checking every frame
    IEnumerator HandleWaveChecks() // could start this with new threshold but then does not account for pausing time
    {
        while (waveLevel + 1 < maxWaveLevel)
        {
            yield return new WaitForSeconds(checkDelay);
            CheckIfShouldLevelUp();
        }
        Debug.Log("Reached Max Enemy Wave!");
    }

    void CheckIfShouldLevelUp() // checks if max wave in coroutine
    {
        if (gameTimeManager.GetTimeElapsed() >= enemyWaves[waveLevel+1].timeThreshold)
        {
            LevelUpEnemies(enemyWaves[waveLevel+1]);
        }
    }

    void LevelUpEnemies(EnemyWave enemyWave) // takes in the new list of enemies
    {
        enemies = enemyWave.newEnemies;

        minSingleSpawn = enemyWave.minSingleSpawn;
        maxSingleSpawn = enemyWave.maxSingleSpawn;

        minSpawnTime = enemyWave.minSpawnTime;
        maxSpawnTime = enemyWave.maxSpawnTime;

        // hoard settings
        hoardData = enemyWave.hoardData;

        waveLevel++;
    }
}
