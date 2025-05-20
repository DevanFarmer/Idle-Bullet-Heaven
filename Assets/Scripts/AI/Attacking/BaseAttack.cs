using UnityEngine;

public abstract class BaseAttack : ScriptableObject
{
    public float power;
    public float speed;
    public float range;

    public virtual void Equip()
    {

    }

    public abstract void Attack(Transform characterTransform, LayerMask targetMask); // Use Weapon?

    public virtual bool InRange(Transform characterTransform, LayerMask targetMask) // runs in Update, virtual in case an attack wants to check in a different way
    {
        Collider2D[] hits = GetHitsInRange(characterTransform, targetMask);
        if (hits.Length > 0) return true;
        else return false;
    }

    protected Collider2D[] GetHitsInRange(Transform characterTransform, LayerMask targetMask)
    {
        return Physics2D.OverlapCircleAll(characterTransform.position, range, targetMask.value);// check if .value is what overlay wants
    }
}
