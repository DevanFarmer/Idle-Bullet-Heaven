using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackHolder : MonoBehaviour
{
    [SerializeField] BaseAttack attack; // list be a list, but need to think how to handle each timer
    [SerializeField] LayerMask targetMask;

    public UnityEvent<bool> onInRangeChanged;

    float lastAttackTime;

    bool inRange;

    void Start()
    {
        attack.Equip();
        lastAttackTime = Time.time;

        inRange = false;
    }

    void Update()
    {
        CheckInRange();

        if (inRange && lastAttackTime + attack.speed <= Time.time)
        {
            attack.Attack(transform, targetMask);
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
}
