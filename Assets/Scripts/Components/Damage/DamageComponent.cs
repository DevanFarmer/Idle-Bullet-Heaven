using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageComponent : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] string enemyTag; // might be a better way but this is fine for now
    [SerializeField] float damage;

    [Header("Hit Events")]
    public UnityEvent OnHit;

    string collisionName;
    List<GameObject> hitObjects = new List<GameObject>();

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == enemyTag)
        {
            if (hitObjects.Contains(collision.gameObject)) return;

            collisionName = collision.gameObject.name;

            HealthComponent health = collision.GetComponent<HealthComponent>();
            if (health != null)
            {
                OnHit?.Invoke();

                health.TakeDamage(damage);
            }

            hitObjects.Add(collision.gameObject);
        }
    }

    public void OnHitDisplay()
    {
        Debug.Log($"{gameObject.name} hit {collisionName} for {damage} damage!");
    }

    public void SetDamage(float damage, bool additive = false)
    {
        if (additive)
        {
            this.damage += damage;
        }
        else this.damage = damage;
    }

    public void SetEnemyTag(string enemyTag)
    {
        this.enemyTag = enemyTag;
    }
}
