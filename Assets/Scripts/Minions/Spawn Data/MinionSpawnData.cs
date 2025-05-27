using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MinionSpawnData", menuName = "Minion/Spawn Data")]
public class MinionSpawnData : ScriptableObject // make a base spawn data so with name, prefab etc and us as when handling in spawning managers
{
    public GameObject minionPrefab;
    public CharacterStats stats; // will store minionstats
    public float spawnCooldown;
    public List<Perk> perks = new();

    // could set base stats here instead of manager, but having them in one place feels better
}
