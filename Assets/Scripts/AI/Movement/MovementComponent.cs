using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementComponent : MonoBehaviour, ITargetObserver
{
    [SerializeField] Transform targetPos;
    Rigidbody2D rb;

    [SerializeField] float moveSpeed;
    [SerializeField] float minDistance; // originally meant for stopping when in attack range but now is kinda useless

    [SerializeField] bool canMove;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;

        canMove = true;
    }

    private void FixedUpdate()
    {
        if (targetPos == null) return;
        if (!canMove) return;
        if (Vector3.Distance(transform.position, targetPos.position) > minDistance) 
            Move();
    }

    void Move()
    {
        Vector3 direction = targetPos.position - transform.position;

        rb.MovePosition(transform.position + direction.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    // inverted for in range, so when in range don't move, when not in range can move
    public void SetCanMoveInverted(bool canMove)
    {
        this.canMove = !canMove;
    }

    public void SetTarget(Transform target)
    {
        this.targetPos = target;
    }
}
