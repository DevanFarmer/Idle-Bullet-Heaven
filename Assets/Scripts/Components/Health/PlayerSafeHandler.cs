using EventBusEventData;
using UnityEngine;

public class PlayerSafeHandler : MonoBehaviour
{
    private void OnEnable()
    {
        EventBus.Subscribe<PlayerHitEvent>(OnPlayerHit);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<PlayerHitEvent>(OnPlayerHit);
    }

    [SerializeField] float safeTime;
    float lastCheckTime;

    bool isSafe;
    bool eventPublished;

    void Start()
    {
        isSafe = true;
        eventPublished = true;
        lastCheckTime = Time.time;
    }

    void Update()
    {
        if (isSafe) return;
        if (eventPublished) return;

        if (lastCheckTime + safeTime <= Time.time)
        {
            EventBus.Publish(new PlayerSafeEvent());
            isSafe = true;
            eventPublished = true;
            // should'nt need to reset last check time since that is handled onPlayerHit event
            // and only checks if not safe, which only happens when the player was hit.
        }
    }

    void OnPlayerHit(PlayerHitEvent e)
    {
        isSafe = false;
        eventPublished = false;
        lastCheckTime = Time.time;
    }
}
