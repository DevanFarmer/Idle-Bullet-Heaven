using EventBusEventData;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawnManager : MonoBehaviour
{
    [SerializeField] List<MinionSpawnData> minions = new();
    [SerializeField] int maxActiveMinions;
    [SerializeField] int numberOfActiveMinions;

    private void OnEnable()
    {
        EventBus.Subscribe<MinionSummonEvent>(OnMinionSummon);
        EventBus.Subscribe<MinionDeathEvent>(OnMinionDeath);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<MinionSummonEvent>(OnMinionSummon);
        EventBus.Unsubscribe<MinionDeathEvent>(OnMinionDeath);
    }

    private void Update()
    {
        HandleInput();
    }

    // will probably use new input system but for now use legacy
    void HandleInput()
    {
        if (numberOfActiveMinions >= maxActiveMinions) return;

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
        GameObject minion = spawnData.Spawn(mouseScreenPosition);

        EventBus.Publish(new MinionSummonEvent(minion));
    }

    void OnMinionSummon(MinionSummonEvent e)
    {
        numberOfActiveMinions++;
    }

    void OnMinionDeath(MinionDeathEvent e)
    {
        numberOfActiveMinions--;
    }
}
