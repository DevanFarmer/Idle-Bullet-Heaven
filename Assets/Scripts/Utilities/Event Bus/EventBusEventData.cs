using UnityEngine;

namespace EventBusEventData
{
    #region Enemy Events
    public readonly struct EnemyHitEvent
    {
        public readonly GameObject Enemy;
        public readonly float Damage;

        public EnemyHitEvent(GameObject enemy, float damage)
        {
            Enemy = enemy;
            Damage = damage;
        }
    }

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

    public readonly struct EnemyStatModifiedEvent
    {
        public readonly StatType statType;
        public readonly float value;

        public EnemyStatModifiedEvent(StatType statType, float value)
        {
            this.statType = statType;
            this.value = value;
        }
    }
    #endregion

    #region Player Events
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

    public readonly struct PlayerDeathEvent { }

    public readonly struct PlayerStatModifiedEvent
    {
        public readonly StatType statType;
        public readonly float value;

        public PlayerStatModifiedEvent(StatType statType, float value)
        {
            this.statType = statType;
            this.value = value;
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
    #endregion

    #region Minion Events
    public readonly struct MinionSummonEvent
    {
        public readonly GameObject Minion;

        public MinionSummonEvent(GameObject minion)
        {
            Minion = minion;
        }
    }

    public readonly struct MinionHitEvent
    {
        public readonly float Damage;

        public MinionHitEvent(float damage)
        {
            Damage = damage;
        }
    }

    public readonly struct MinionDeathEvent
    {
        public readonly GameObject Minion;
        public readonly float Damage;

        public MinionDeathEvent(GameObject minion, float damage)
        {
            Minion = minion;
            Damage = damage;
        }
    }

    public readonly struct MinionStatModifiedEvent
    {
        public readonly StatType statType;
        public readonly float value;

        public MinionStatModifiedEvent(StatType statType, float value)
        {
            this.statType = statType;
            this.value = value;
        }
    }
    #endregion
}
