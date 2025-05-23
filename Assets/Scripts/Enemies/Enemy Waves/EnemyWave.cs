using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Spawning/Enemy Wave")]
public class EnemyWave : ScriptableObject
{
    [Header("Threshold")]
    public float timeThreshold;

    [Header("Enemies")]
    public List<EnemySpawnData> newEnemies = new List<EnemySpawnData>();

    [Header("Single Spawn Numbers")]
    public int minSingleSpawn;
    public int maxSingleSpawn;

    [Header("Spawn Times")]
    public float minSpawnTime;
    public float maxSpawnTime;

    [Header("Hoard Data")]
    public HoardData hoardData;
}
