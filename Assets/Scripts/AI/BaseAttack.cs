using UnityEngine;

public abstract class BaseAttack : ScriptableObject
{
    public float attackPower;
    public float range;

    // could store transform of character and layerMask
    protected Transform characterTransform;
    protected LayerMask targetMask;

    public virtual void Equip(Transform transform, LayerMask mask)
    {
        characterTransform = transform;
        targetMask = mask;
    }

    public abstract void Attack(); // Use Weapon?

    public virtual bool InRange()
    {
        Collider2D[] hits = GetHitsInRange();
        if (hits.Length > 0) return true;
        else return false;
    }

    protected Collider2D[] GetHitsInRange()
    {
        return Physics2D.OverlapCircleAll(characterTransform.position, range, targetMask.value);// check if .value is what overlay wants
    }
}
