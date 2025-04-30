using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementComponent : MonoBehaviour
{
    Vector3 targetPos;
    Rigidbody2D rb;

    [SerializeField] float moveSpeed;
    [SerializeField] float minDistance;

    // could make StatManager not a singlton and find the players StatManager in GameManager 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, targetPos) > minDistance) 
            Move();
    }

    void Move()
    {
        Vector3 direction = targetPos - transform.position;

        rb.MovePosition(transform.position + direction * moveSpeed * Time.fixedDeltaTime);
    }
}
