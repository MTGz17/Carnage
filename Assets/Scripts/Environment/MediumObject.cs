using UnityEngine;

public class MediumObject : MonoBehaviour
{
    private Rigidbody rb;
    public float launchForce = 8f;
    public int pointsOnDestroy = 50;
    public float destroyDelay = 0.2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        Vector3 impactDirection = collision.relativeVelocity.normalized;
        rb.AddForce(impactDirection * launchForce, ForceMode.Impulse);

        if (collision.relativeVelocity.magnitude >= 11.17f)
        {
            if (ScoreManager.Instance != null)
                {
                    ScoreManager.Instance.AddPoints(pointsOnDestroy);
                }
            Destroy(gameObject, destroyDelay);
        }
    }
}