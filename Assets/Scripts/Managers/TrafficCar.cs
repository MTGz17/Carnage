using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TrafficCar : MonoBehaviour
{
    [Header("Car Settings")]
    public float initialSpeed = 10f;   // Base initial force
    public float maxSpeed = 20f;       // Optional: cap maximum speed
    public float drag = 0.5f;          // Adjust to simulate friction / slowing

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.linearDamping = drag; // slows the car naturally over time
    }

    private void Start()
    {
        // Apply one-time initial impulse
        rb.AddForce(Vector3.left * initialSpeed, ForceMode.VelocityChange);
    }

    private void FixedUpdate()
    {
        // Optional: clamp max speed to prevent runaway acceleration
        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (horizontalVelocity.magnitude > maxSpeed)
        {
            horizontalVelocity = horizontalVelocity.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(horizontalVelocity.x, rb.linearVelocity.y, horizontalVelocity.z);
        }

        // Destroy offscreen cars
        if (transform.position.x < -50f)
            Destroy(gameObject);
    }
}