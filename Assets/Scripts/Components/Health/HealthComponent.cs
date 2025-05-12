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

    StatManager statManager;

    public Action onDeath;

    bool isDead;

    void Start()
    {
        isDead = false;

        statManager = GetComponent<StatManager>(); // check if null
        UpdateMaxHealth();

        switch (characterType) 
        {
            case CharacterType.Player:
                EventBus.Subscribe<PlayerStatModifiedEvent>(OnPlayerStatModified);
                onDeath += () => { EventBus.Publish(new PlayerDeathEvent()); };
                break;
            case CharacterType.Enemy:
                EventBus.Subscribe<EnemyStatModifiedEvent>(OnEnemyStatModified);
                onDeath += () => { EventBus.Publish(new EnemyDeathEvent()); };
                break;
            case CharacterType.Minion:
                EventBus.Subscribe<MinionStatModifiedEvent>(OnMinionStatModified);
                onDeath += () => { EventBus.Publish(new MinionStatModifiedEvent()); };
                break;
        }

        currentHealth = maxHealth;
    }

    public bool TakeDamage(float damage, bool hit = true)
    {
        if (HandleInvincibilityHits() && hit) return false;

        currentHealth -= damage;

        HandleCharacterHitEvents(damage);

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

    void OnPlayerStatModified(PlayerStatModifiedEvent e)
    {
        if (e.statType != StatType.Health) return;
        UpdateMaxHealth();
    }

    void OnEnemyStatModified(EnemyStatModifiedEvent e)
    {
        if (e.statType != StatType.Health) return;
        UpdateMaxHealth();
    }

    void OnMinionStatModified(MinionStatModifiedEvent e)
    {
        if (e.statType != StatType.Health) return;
        UpdateMaxHealth();
    }

    void UpdateMaxHealth()
    {
        maxHealth = statManager.GetCalculatedStat(StatType.Health);
    }

    public float GetMaxHealth() { return maxHealth; }

    public float GetCurrentHealth() { return currentHealth; }

    void HandleCharacterHitEvents(float damage)
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
