namespace EventBusEventData
{
    public readonly struct EnemyDeathEvent
    {
        public readonly float Damage;

        public EnemyDeathEvent(float damage)
        {
            Damage = damage;
        }
    }
}
