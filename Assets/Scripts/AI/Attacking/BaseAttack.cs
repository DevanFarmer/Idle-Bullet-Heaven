using UnityEngine;

public abstract class BaseAttack : ScriptableObject
{
    public float power;
    public float attackSpeed;
    public float range;

    public virtual void Equip(Transform character)
    {
        
    }

    public virtual void Attack(Transform character, Transform target, StatManager statManager) // Use Weapon?
    {
        // use weapon
    }

    public virtual bool InRange(Transform characterTransform, LayerMask targetMask) // runs in Update, virtual in case an attack wants to check in a different way
    {
        Collider2D[] hits = GetHitsInRange(characterTransform, targetMask);
        if (hits.Length > 0) return true;
        else return false;
    }

    public Collider2D[] GetHitsInRange(Transform characterTransform, LayerMask targetMask)
    {
        return Physics2D.OverlapCircleAll(characterTransform.position, range, targetMask.value);// check if .value is what overlay wants
    }

    protected float GetDamage(StatManager statManager)
    {
        return power + statManager.GetCalculatedStat(StatType.AttackPower);
    }
}
