using EventBusEventData;
using UnityEngine;

public class ShieldComponent : MonoBehaviour
{
    HealthComponent healthComponent;
    Collider2D shieldCollider;
    SpriteRenderer gfx;
    [SerializeField] Collider2D playerCollider;

    [SerializeField] float regenTick;
    [SerializeField] float regenAmount;

    bool regenShield;
    float lastHealTick;

    private void Start()
    {
        shieldCollider = GetComponent<Collider2D>();

        healthComponent = GetComponent<HealthComponent>();
        healthComponent.onDeath += () => { EventBus.Publish(new ShieldBroke()); };

        gfx = GetComponentInChildren<SpriteRenderer>();

        regenShield = true;
        lastHealTick = Time.time;
    }

    private void Update()
    {
        if (!regenShield) return;

        if (lastHealTick + regenTick <= Time.time)
        {
            healthComponent.Heal(regenAmount);
            lastHealTick = Time.time;
        }
    }

    void OnPlayerSafe(PlayerSafeEvent e)
    {
        // restart regen shield health
        regenShield = true;
        shieldCollider.enabled = true;
        playerCollider.enabled = false; // disable while shield to prevent aoe damage
        gfx.enabled = true;
    }

    void OnShieldBroken(ShieldBroke e)
    {
        regenShield = false;
        shieldCollider.enabled = false;
        playerCollider.enabled = true;
        gfx.enabled = false;
    }

    void OnShieldHit(ShieldHitEvent e)
    {
        lastHealTick = Time.time;
    }

    void OnEnable()
    {
        EventBus.Subscribe<PlayerSafeEvent>(OnPlayerSafe);
        EventBus.Subscribe<ShieldBroke>(OnShieldBroken); // could not use but this makes it safer than just using health comp ondeath action
        EventBus.Subscribe<ShieldHitEvent>(OnShieldHit);
    }

    void OnDisable()
    {
        EventBus.Unsubscribe<PlayerSafeEvent>(OnPlayerSafe);
        EventBus.Unsubscribe<ShieldBroke>(OnShieldBroken);
        EventBus.Unsubscribe<ShieldHitEvent>(OnShieldHit);
    }
}
