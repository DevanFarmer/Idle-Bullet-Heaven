using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Spawning/Hoard Data")]
public class HoardData : ScriptableObject
{
    [Header("Enabler")]
    public bool canSpawnHoard;

    [Header("Checking Hoard Spawn")]
    [Range(1, 100)]
    public float hoardChance;
    public float hoardCheckTime;

    [Header("Spawn Numbers")]
    public int minHoardSpawns;
    public int maxHoardSpawns;
}
