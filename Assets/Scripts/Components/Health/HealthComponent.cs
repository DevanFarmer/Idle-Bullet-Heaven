using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{

    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;

    public Action onDeath;

    bool isDead;

    void Start()
    {
        isDead = false;
        currentHealth = maxHealth;
    }

    public bool TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
        {
            Die();
            return true;
        }

        return false;
    }

    void Die()
    {
        onDeath?.Invoke();
        isDead = true;
    }

    public void Heal(float amount)
    {
        if (isDead) return;
        
        currentHealth += amount;

        if (currentHealth > maxHealth) currentHealth = maxHealth;
    }

    public float GetMaxHealth() { return maxHealth; }

    public float GetCurrentHealth() { return currentHealth; }
}
