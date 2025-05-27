using System.Collections.Generic;
using UnityEngine;

public class TargetComponent : MonoBehaviour
{
    Transform target; // will be a list later for multi targeting
    HealthComponent targetHealth;

    [SerializeField] LayerMask targetMask;
    [SerializeField] float detectionRange;

    List<ITargetObserver> targetobservers = new List<ITargetObserver>();

    IStats statManager;

    private void Start()
    {
        statManager = GetComponent<IStats>();
        GetTargetObservers();
    }

    void Update()
    {
        SetDetectionRange(statManager.GetCalculatedStat(StatType.DetectionRange));
        if (target != null && targetHealth.IsAlive()) return;

        GetNewTarget(transform, targetMask);
    }

    private void GetNewTarget(Transform characterTransform, LayerMask targetMask)
    {
        Collider2D[] hits = GetHitsInRange(characterTransform, targetMask);
        if (hits.Length > 0)
        {
            int randomIndex = Random.Range(0, hits.Length);
            target = hits[randomIndex].transform;
            NotifyObservers(target);
            targetHealth = target.GetComponent<HealthComponent>();
        }
    }

    private Collider2D[] GetHitsInRange(Transform characterTransform, LayerMask targetMask)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(characterTransform.position, detectionRange, targetMask.value);// check if .value is what overlay wants
        List<Collider2D> validHits = new();
        foreach (Collider2D hit in hits)
        {
            // only consider hits with health comp and is alive
            HealthComponent hitHealth = hit.GetComponent<HealthComponent>();
            if (hitHealth == null) continue;
            if (!hitHealth.IsAlive()) continue;
            validHits.Add(hit);
        }
        return validHits.ToArray();
    }

    public void SetDetectionRange(float range)
    {
        if (range < 0) { Debug.LogWarning($"Tried to updated detection range on {gameObject.name} but value passed was less than zero!"); return; }
        detectionRange = range;
    }

    void GetTargetObservers()
    {
        ITargetObserver[] _targetObservers = GetComponents<ITargetObserver>();
        foreach (ITargetObserver targetOberver in _targetObservers)
        {
            AddObserver(targetOberver);
        }
    }

    void AddObserver(ITargetObserver _observer)
    {
        targetobservers.Add(_observer);
    }

    void NotifyObservers(Transform target)
    {
        foreach (ITargetObserver targetObserver in targetobservers)
        {
            targetObserver.SetTarget(target);
        }
    }
}
