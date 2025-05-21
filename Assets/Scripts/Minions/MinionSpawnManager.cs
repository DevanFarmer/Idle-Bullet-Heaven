using EventBusEventData;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawnManager : MonoBehaviour
{
    [SerializeField] List<MinionSpawnData> minions = new();

    private void Update()
    {
        HandleInput();
    }

    // will probably use new input system but for now use legacy
    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpawnMinion(minions[0]);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            SpawnMinion(minions[1]);
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            SpawnMinion(minions[2]);
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            SpawnMinion(minions[3]);
        }
    }

    void SpawnMinion(MinionSpawnData spawnData)
    {
        // get mouse position
        Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseScreenPosition.z = 0;

        // spawn prefab
        GameObject minion = Instantiate(spawnData.minionPrefab, mouseScreenPosition, Quaternion.identity);

        // set stats
        minion.GetComponent<StatManager>().SetCharacterStats(spawnData.stats);

        // add perks
        PerkManager perkManager = minion.GetComponent<PerkManager>();
        foreach(Perk perk in spawnData.perks)
        {
            perkManager.GainPerk(perk);
        }

        // add death
        HealthComponent health = minion.GetComponent<HealthComponent>();
        health.onDeath += () => { Destroy(minion); };

        EventBus.Publish(new MinionSummonEvent(minion));
    }
}
