using System.Collections.Generic;
using UnityEngine;

public class MinionSpawnData : ScriptableObject
{
    public GameObject minionPrefab;
    public CharacterStats stats; // will store minionstats
    // if want to use cooldown on spawns have set it here

    // could set base stats here instead of manager, but having them in one place feels better
}
