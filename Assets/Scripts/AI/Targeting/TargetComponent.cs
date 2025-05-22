using System.Collections.Generic;
using UnityEngine;

public class TargetComponent : MonoBehaviour
{
    Transform target; // will be a list later for multi targeting

    [SerializeField] LayerMask targetMask;
    [SerializeField] float detectionRange;

    List<ITargetObserver> targetobservers = new List<ITargetObserver>();

    private void Start()
    {
        GetTargetObservers();
    }

    void Update()
    {
        if (target != null) return;

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
        }
    }

    private Collider2D[] GetHitsInRange(Transform characterTransform, LayerMask targetMask)
    {
        return Physics2D.OverlapCircleAll(characterTransform.position, detectionRange, targetMask.value);// check if .value is what overlay wants
    }

    public Transform GetTarget()
    {
        return target;
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
