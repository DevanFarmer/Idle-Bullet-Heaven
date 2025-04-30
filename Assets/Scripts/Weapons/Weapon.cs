using UnityEngine;

public abstract class Weapon : Perk
{
    public GameObject weaponPrefab;

    public float power;
    public float attackSpeed; // Cooldown
    public float range;
}
