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

    [Header("Other")]
    [SerializeField] bool canHealAfterDeath;

    IStats statManager;

    public Action<float, Transform> onHit;
    public Action onDeath;

    bool isDead;

    void Start()
    {
        isDead = false;

        if (statManager == null) statManager = GetComponent<IStats>(); // check if null
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
                onDeath += () => { EventBus.Publish(new MinionDeathEvent()); };
                break;
            case CharacterType.Shield:
                EventBus.Subscribe<ShieldStatChanged>(OnShieldStatModified);
                break;
        }

        currentHealth = maxHealth;
    }

    public bool TakeDamage(float damage, bool hit = true)
    {
        if (HandleInvincibilityHits() && hit) return false;

        currentHealth -= damage;

        HandleCharacterHitEvents(damage);

        onHit?.Invoke(damage, transform);

        if (currentHealth <= 0)
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
        if (!canHealAfterDeath && isDead) return;
        
        currentHealth += amount;

        if (currentHealth > maxHealth) 
            currentHealth = maxHealth;

        isDead = false;
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

    // should probably check if it the character that was hit else everything gonna update every time hit is published
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

    void OnShieldStatModified(ShieldStatChanged e)
    {
        if (e.statType != StatType.Health) return;
        UpdateMaxHealth();
    }

    // making these public is a temporary bandage fixes
    public void SetStatManager(IStats statManager)
    {
        this.statManager = statManager;
    }

    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    public void UpdateMaxHealth()
    {
        if (statManager == null) 
        {
            Debug.LogWarning($"No stat manager found on {gameObject.name} for {this.name}! Could not update max health!");
            return;
        }
        maxHealth = statManager.GetCalculatedStat(StatType.Health);
    }

    public float GetMaxHealth() { return maxHealth; }

    public float GetCurrentHealth() { return currentHealth; }

    public bool IsAlive()
    {
        return !isDead;
    }

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
            case CharacterType.Shield:
                EventBus.Publish(new ShieldHitEvent(damage));
                break;
        }
    }
}
