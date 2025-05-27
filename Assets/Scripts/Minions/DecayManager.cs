using UnityEngine;

public class DecayManager : MonoBehaviour
{
    HealthComponent health;
    MinionsManager minionsManager;

    [SerializeField] float decayRate;
    [SerializeField] float decayAmount;
    [SerializeField] ModifierType amountValueType;

    float lastDecayTick;

    IStats statManager;

    void Start()
    {
        minionsManager = MinionsManager.Instance;
        health = GetComponent<HealthComponent>();
        statManager = GetComponent<IStats>();
        lastDecayTick = Time.time;
    }

    void Update()
    {
        decayAmount = statManager.GetCalculatedStat(StatType.Health);
        decayRate = statManager.GetCalculatedStat(StatType.DecayRate);

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
        if (amountValueType == ModifierType.Percentage) amount = health.GetMaxHealth() * (decayAmount / 100f);
        health.TakeDamage(amount, false);
    }
}
