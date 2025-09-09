using UnityEngine;

public class MediumObject : MonoBehaviour
{
    private Rigidbody rb;
    public float launchForce = 8f;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        Vector3 impactDirection = collision.relativeVelocity.normalized;
        rb.AddForce(impactDirection * launchForce, ForceMode.Impulse);
    }
}