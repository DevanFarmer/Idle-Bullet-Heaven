using UnityEngine;

namespace EventBusEventData
{
    public readonly struct EnemyDeathEvent
    {
        public readonly GameObject Enemy;
        public readonly float Damage;

        public EnemyDeathEvent(GameObject enemy, float damage)
        {
            Enemy = enemy;
            Damage = damage;
        }
    }

    public readonly struct PlayerHitEvent
    {
        public readonly float Damage;

        public PlayerHitEvent(float damage)
        {
            Damage = damage;
        }
    }
}
