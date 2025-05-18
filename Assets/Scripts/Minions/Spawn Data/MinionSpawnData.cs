using System.Collections.Generic;
using UnityEngine;

public class MinionSpawnData : ScriptableObject
{
    public GameObject minionPrefab;
    public CharacterStats stats; // will store minionstats
    public float spawnCooldown;
    public List<Perk> perks = new();

    // could set base stats here instead of manager, but having them in one place feels better
}
