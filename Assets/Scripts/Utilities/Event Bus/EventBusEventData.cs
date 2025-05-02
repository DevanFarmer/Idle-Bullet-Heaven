using UnityEngine;

namespace EventBusEventData
{
    public readonly struct PlayerHitEvent
    {
        public readonly GameObject Enemy;
        public readonly float Damage;

        public PlayerHitEvent(GameObject enemy, float damage)
        {
            Enemy = enemy;
            Damage = damage;
        }
    }

    public readonly struct PlayerDamagedEvent
    {
        public readonly float Damage;

        public PlayerDamagedEvent(float damage)
        {
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

    public readonly struct PlayerHealEvent
    {
        public readonly float HealAmount;

        public PlayerHealEvent(float healAmount)
        {
            HealAmount = healAmount;
        }
    }

    public readonly struct PlayerSafeEvent { }

    public readonly struct LevelUpEvent
    {
        public readonly int Level;

        public LevelUpEvent(int level)
        {
            Level = level;
        }
    }

    public readonly struct CriticalHitEvent
    {
        public readonly float Critical;

        public CriticalHitEvent(float critical)
        {
            Critical = critical;
        }
    }

    public readonly struct ReviveEvent { }
}
