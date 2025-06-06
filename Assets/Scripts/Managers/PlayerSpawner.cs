using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] PlayerSpawnData spawnData; // how will this be set before spawning? When selecting player character?

    // could store perk manager and stat manager here?

    public GameObject SpawnPlayer(Vector3 spawnPos)
    {
        if (spawnData == null) { Debug.LogError($"No spawn data set in {this.name} on {gameObject.name}! Could not spawn player!"); return null; }

        return spawnData.Spawn(spawnPos);
    }
}
