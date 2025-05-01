using UnityEngine;

public abstract class Weapon : Perk
{
    public GameObject weaponPrefab;

    public float power;
    public float attackSpeed; // Cooldown
    public float range;

    protected float lastAttackTime;

    // abstract Attack method?
    // can then override OnUpdate with:
    //if (lastAttackTime + attackSpeed <= Time.time)
    //{
            //Attack();
    //}
}
