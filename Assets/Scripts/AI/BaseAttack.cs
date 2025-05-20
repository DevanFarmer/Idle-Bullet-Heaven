using UnityEngine;

public abstract class BaseAttack : ScriptableObject
{
    public float power;
    public float speed;
    public float range;

    // could store transform of character and layerMask
    protected Transform characterTransform;
    protected LayerMask targetMask;

    public virtual void Equip(Transform characterTransform, LayerMask targetMask)
    {
        this.characterTransform = characterTransform;
        this.targetMask = targetMask;
    }

    public abstract void Attack(); // Use Weapon?

    public virtual bool InRange() // runs in Update
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
