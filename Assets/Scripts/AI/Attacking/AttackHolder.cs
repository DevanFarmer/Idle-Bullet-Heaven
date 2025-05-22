using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackHolder : MonoBehaviour, ITargetObserver
{
    [SerializeField] BaseAttack attack; // list be a list, but need to think how to handle each timer
    [SerializeField] LayerMask targetMask; // get from target comp

    public UnityEvent<bool> onInRangeChanged;

    float lastAttackTime;

    bool inRange;

    Transform target; // store in a middle man class since attack and movement need it
    StatManager stats;

    void Start()
    {
        stats = GetComponent<StatManager>();

        attack.Equip(transform);
        lastAttackTime = Time.time;

        inRange = false;
    }

    void Update()
    {
        if (target == null) return;

        CheckInRange();

        if (inRange && lastAttackTime + attack.attackSpeed <= Time.time)
        {
            attack.Attack(transform, target, stats);
            lastAttackTime = Time.time;
        }
    }

    void CheckInRange()
    {
        bool rangeState = attack.InRange(transform, targetMask);

        if (rangeState != inRange)
        {
            inRange = rangeState;
            onInRangeChanged?.Invoke(inRange);
        }
    }

    public void TestInRangeMethod()
    {
        Debug.Log($"Ping {inRange}! From {this.name} on {gameObject.name}");
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
