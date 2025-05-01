using System.Collections.Generic;
using UnityEngine;
using SpawningUtilities;

public class DirectionalSpawnAttack : Weapon
{
    public enum DirectionType { TowardMouse, AwayFromMouse, Forward, Backward}

    [Tooltip("Spawn range from center.")]
    public new float range;
    public DirectionType directionType;
    public SpawnType spawnType;

    public override void OnUpdate(GameObject owner)
    {
        if (lastAttackTime + attackSpeed <= Time.time)
        {
            HandleAttack();
        }
    }

    void HandleAttack()
    {
        switch (directionType)
        {
            case DirectionType.TowardMouse:

                break;
            case DirectionType.AwayFromMouse:

                break;
            case DirectionType.Forward:

                break;
            case DirectionType.Backward:

                break;
        }
    }
}
