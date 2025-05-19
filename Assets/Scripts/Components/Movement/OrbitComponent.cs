using UnityEngine;

// bug with the script: it always move toward the maxRadius which does not account for the angle difference
public class OrbitComponent : MonoBehaviour
{
    private Transform center;

    [SerializeField] float orbitSpeed;
    [SerializeField] int numberOfOrbits;

    bool spawnAtCenter;
    bool returnToCenter;

    private float rotatedDegrees = 0f;
    private float totalDegreesToRotate;
    private float currentRadius = 0f;
    private float angle = 0f;

    [SerializeField] float moveSpeed; // Moving to/from center
    [SerializeField] float maxRadius;
    enum State { Expanding, Contracting }
    State state = State.Expanding;

    public void InitializeComponent(Transform center, float orbitSpeed, int numberOfOrbits, 
        float maxRadius, float moveSpeed, 
        bool spawnAtCenter = false, bool returnToCenter = false)
    {
        this.center = center;
        this.orbitSpeed = orbitSpeed;
        this.numberOfOrbits = numberOfOrbits;
        this.maxRadius = maxRadius;
        this.moveSpeed = moveSpeed;
        this.spawnAtCenter = spawnAtCenter;
        this.returnToCenter = returnToCenter;
    }

    void Start()
    {
        // Move to InitailizeComponent?

        if (center == null)
            center = transform.parent;

        totalDegreesToRotate = 360f * numberOfOrbits;
        currentRadius = spawnAtCenter ? 0f : maxRadius;

        // Start at correct position
        angle = transform.eulerAngles.z; // important for changing the "start" of the orbit
        UpdatePosition();
    }

    void Update()
    {
        float deltaAngle = orbitSpeed * Time.deltaTime;
        angle += deltaAngle;
        rotatedDegrees += deltaAngle;

        // Radius control
        if (state == State.Expanding)
        {
            currentRadius = Mathf.MoveTowards(currentRadius, maxRadius, moveSpeed * Time.deltaTime);
        }
        else if (state == State.Contracting)
        {
            currentRadius = Mathf.MoveTowards(currentRadius, 0f, moveSpeed * Time.deltaTime);
        }

        UpdatePosition();

        // Check for transition
        if (rotatedDegrees >= totalDegreesToRotate)
        {
            if (returnToCenter && state == State.Expanding)
            {
                state = State.Contracting;
                rotatedDegrees = 0f; // optional: count circles again inward
            }
            else if (!returnToCenter)
            {
                Destroy(gameObject);
                return;
            }
        }

        if (state == State.Contracting)
        {
            if (returnToCenter && Vector3.Distance(transform.parent.position, transform.position) < 0.1f)
            {
                Destroy(gameObject);
                return;
            }
        }
    }

    void UpdatePosition()
    {
        float radians = angle * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0) * currentRadius;
        transform.position = center.position + offset;
    }
}
