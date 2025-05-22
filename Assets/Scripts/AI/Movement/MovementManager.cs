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

    
}
