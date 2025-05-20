using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [SerializeField] float detectionRange;
    [SerializeField] LayerMask targetMask;

    MovementComponent moveComponent;

    // can add roaming functionality here

    void Start()
    {
        moveComponent = GetComponent<MovementComponent>();
    }

    void Update()
    {
        if (moveComponent.HasTarget()) return;

        GetNewTarget(transform, targetMask);
    }

    public void GetNewTarget(Transform characterTransform, LayerMask targetMask) // runs in Update, virtual in case an attack wants to check in a different way
    {
        Collider2D[] hits = GetHitsInRange(characterTransform, targetMask);
        if (hits.Length > 0)
        {
            int randomIndex = Random.Range(0, hits.Length);
            moveComponent.SetTarget(hits[randomIndex].transform);
        } 
    }

    protected Collider2D[] GetHitsInRange(Transform characterTransform, LayerMask targetMask)
    {
        return Physics2D.OverlapCircleAll(characterTransform.position, detectionRange, targetMask.value);// check if .value is what overlay wants
    }
}
