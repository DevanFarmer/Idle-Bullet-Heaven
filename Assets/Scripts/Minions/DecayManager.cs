using UnityEngine;

public class DecayManager : MonoBehaviour
{
    HealthComponent health;
    MinionsManager minionsManager;

    [SerializeField] float decayRate;
    [SerializeField] float decayAmount;
    [SerializeField] PassiveValueType amountValueType;

    float lastDecayTick;

    void Start()
    {
        minionsManager = MinionsManager.Instance;
        health = GetComponent<HealthComponent>();
        lastDecayTick = Time.time;
    }

    void Update()
    {
        if (!minionsManager.minionsDecay) return;

        if (lastDecayTick + decayRate <= Time.time)
        {
            Decay();
            lastDecayTick = Time.time;
        }
    }

    void Decay()
    {
        float amount = decayAmount;
        if (amountValueType == PassiveValueType.Percentage) amount = health.GetMaxHealth() * (decayAmount / 100f);
        health.TakeDamage(amount, false);
    }
}
