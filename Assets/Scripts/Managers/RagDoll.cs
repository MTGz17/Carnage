using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RagdollOnCollision : MonoBehaviour
{
    [Header("Ragdoll Settings")]
    public float postCrashDrag = 0.1f;       // How much drag after impact
    public float postCrashAngularDrag = 0.05f; // Spin resistance after impact
    public float despawnTime = 0f;           // Optional: auto-destroy after X seconds (0 = never)

    private Rigidbody rb;
    private bool hasCrashed = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasCrashed) return; // Prevents double-triggering
        hasCrashed = true;

        // Let physics fully take over
        rb.linearDamping = postCrashDrag;
        rb.angularDamping = postCrashAngularDrag;

        // Optional timed cleanup
        if (despawnTime > 0f)
            Destroy(gameObject, despawnTime);
    }
}