using UnityEngine;

[CreateAssetMenu(menuName = "Spawn Data/Base")]
public class BaseSpawnData : ScriptableObject
{
    public GameObject spawnPrefab;

    public virtual GameObject Spawn(Vector3 spawnPos)
    {
        GameObject spawnobject = Instantiate(spawnPrefab, spawnPos, Quaternion.identity);
        return spawnobject;
    }
}
