using EventBusEventData;
using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;

    [Header("Invincibility")]
    [SerializeField] int invincibilityHits;

    [Header("Character Type")]
    [SerializeField] CharacterType characterType;

    public Action onDeath;

    bool isDead;

    void Start()
    {
        isDead = false;
        currentHealth = maxHealth;
    }

    public bool TakeDamage(float damage, bool hit = true)
    {
        if (HandleInvincibilityHits() && hit) return false;

        currentHealth -= damage;

        HandleHitEvents(damage);

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

    void HandleHitEvents(float damage)
    {
        switch (characterType)
        {
            case CharacterType.Player:
                EventBus.Publish(new PlayerHitEvent(damage));
                break;
            case CharacterType.Minion:
                EventBus.Publish(new MinionHitEvent(damage));
                break;
            case CharacterType.Enemy:
                EventBus.Publish(new EnemyHitEvent(gameObject, damage));
                break;
        }
    }
}
