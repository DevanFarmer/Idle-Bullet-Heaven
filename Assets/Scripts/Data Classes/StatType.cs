public enum StatType
{
    // Base/All
    Health,
    AttackPower,
    AttackSpeed,
    CriticalChance,
    AttackRange,
    AttackSize,
    MoveSpeed,

    // Player Specific
    XPMultiplier,
    InvincibilityHits, // have in own script or something
    ShieldHealth,
    ShieldDamage,
    MaxActiveMinions,
    MinionSquadNumber,
    Revives,

    // Player-Minion
    ProjectileBounces,
    ProjectilePierceNumber,

    // Minion
    DecayRate,
    DetectionRange
}
