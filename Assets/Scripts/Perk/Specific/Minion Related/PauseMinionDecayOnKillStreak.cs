using EventBusEventData;
using UnityEngine;

public class PauseMinionDecayOnKillStreak : Perk
{
    public int enemiesNeededToTrigger;
    int totalEnemiesKilled;

    public float pauseTime;

    public override void OnEquip(GameObject owner)
    {
        base.OnEquip(owner);

        EventBus.Subscribe<EnemyDeathEvent>(OnEnemyDeath);
    }

    public override void OnUnEquip(GameObject owner)
    {
        base.OnUnEquip(owner);

        EventBus.Unsubscribe<EnemyDeathEvent>(OnEnemyDeath);
    }

    void OnEnemyDeath(EnemyDeathEvent e)
    {
        totalEnemiesKilled++;

        if (totalEnemiesKilled % enemiesNeededToTrigger == 0) 
            HandleDecay();
    }

    void HandleDecay()
    {
        MinionsManager.Instance.PauseMinionsDecay(pauseTime);
    }
}
