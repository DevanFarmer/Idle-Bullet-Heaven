using UnityEngine;

public abstract class Weapon : ScriptableObject
{
    [Header("General")]
    public string weaponName;
    [TextArea(1, 10)]
    public string description;

    [Header("Set up")]
    public GameObject weaponPrefab;

    public float power;
    public float attackSpeed; // "Cooldown"
    public float range; // is this needed for all weapons? Each has something similar but not the same name

    protected float lastAttackTime;

    protected IStats statManager;

    public virtual void OnObtained(GameObject owner)
    {
        statManager = owner.GetComponent<IStats>();
        lastAttackTime = Time.time;
    }

    public virtual void OnUpdate(GameObject owner)
    {
        if (lastAttackTime + attackSpeed <= Time.time)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    protected virtual void Attack()
    {

    }
}
