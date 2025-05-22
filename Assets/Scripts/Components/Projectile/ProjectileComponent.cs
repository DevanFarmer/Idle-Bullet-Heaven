using UnityEngine;

// This component is used to make the gameobject move like a homing missile
[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileComponent : MonoBehaviour, ITargetObserver // could make a base monobehaviour that stores same stuff as movement component 
{
    // Could be used as a middle man to set components like damage comp
    [SerializeField] Transform target;
    [SerializeField] float moveSpeed;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    private void FixedUpdate()
    {
        if (target != null) Move();
        else Destroy(gameObject);
    }

    void Move()
    {
        Vector3 direction = target.position - transform.position;

        rb.MovePosition(transform.position + direction.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    public void SetMoveSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
