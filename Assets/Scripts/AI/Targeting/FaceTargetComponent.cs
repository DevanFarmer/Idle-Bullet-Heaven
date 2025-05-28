using UnityEngine;

public class FaceTargetComponent : MonoBehaviour, ITargetObserver
{
    [SerializeField] Transform target;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    void FixedUpdate() // maybe even use fixed update since don't need to be that accuray when flipping
    {
        if (target == null) return;

        UpdateFaceDirection();
    }

    void UpdateFaceDirection()
    {
        // could check if already flipped the right way to not constantly try to flip, not sure if it would make a difference ngl
        if (transform.position.x > target.position.x) // left of character
        {
            spriteRenderer.flipX = false;
        } 
        else if (transform.position.x < target.position.x) // right of character
        {
            spriteRenderer.flipX = true;
        }
    }
}
