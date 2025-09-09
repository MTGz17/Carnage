using UnityEngine;

public class SmallObject : MonoBehaviour
{
    private Rigidbody rb;
    public float launchForce = 8f;
    public int pointsOnDestroy = 10;
    public float destroyDelay = 0.2f;

    private bool hasBeenHit = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasBeenHit) return;
        if (!collision.gameObject.CompareTag("Player")) return;

        hasBeenHit = true;

        Vector3 impactDirection = collision.relativeVelocity.normalized;
        rb.AddForce(impactDirection * launchForce, ForceMode.Impulse);

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddPoints(pointsOnDestroy);
        }

        Destroy(gameObject, destroyDelay);
    }
}
