using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;

    [Header("Invincibility")]
    [SerializeField] int invincibilityHits;

    public Action onDeath;

    bool isDead;

    void Start()
    {
        isDead = false;
        currentHealth = maxHealth;
    }

    public bool TakeDamage(float damage)
    {
        if (HandleInvincibilityHits()) return false;

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

        if (currentHealth > maxHealth) 
            currentHealth = maxHealth;
    }

    bool HandleInvincibilityHits()
    {
        if (invincibilityHits > 0)
        {
            invincibilityHits--;
            return true;
        }

        return false;
    }

    public float GetMaxHealth() { return maxHealth; }

    public float GetCurrentHealth() { return currentHealth; }
}
