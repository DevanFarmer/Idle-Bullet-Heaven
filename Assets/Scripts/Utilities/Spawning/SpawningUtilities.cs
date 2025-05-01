using UnityEngine;

namespace SpawningUtilities
{
    public enum SpawnType
    {
        Instant,
        MoveToward
    }

    public static class SpawningUtilities
    {
        public static void SetSpawnTime(ref float spawnTime, ref float minSpawnTime, ref float maxSpawnTime)
        {
            spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        }
    }
}
