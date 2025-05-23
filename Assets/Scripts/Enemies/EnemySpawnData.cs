using UnityEngine;

[CreateAssetMenu(fileName = "New EnemySpawnData", menuName = "Enemy Spawning/Spawn Data")]
public class EnemySpawnData : ScriptableObject
{
    // Can make a baseCharacter Class that stores common things like prefab, attack animation etc.
    // Weapon/Attack?

    public GameObject prefab;

    public float expPoints;

    // stats
}
