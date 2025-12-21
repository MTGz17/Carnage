using UnityEngine;

public class SmallObject : MonoBehaviour
{
    private Rigidbody rb;

    public float launchForce = 8f;
    public int pointsOnDestroy = 10;
    public float destroyDelay = 4f;

    private bool isDestroying = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        Vector3 impactDirection = collision.relativeVelocity.normalized;
        rb.AddForce(impactDirection * launchForce, ForceMode.Impulse);

        if (isDestroying) return;
        isDestroying = true;

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddPoints(pointsOnDestroy);
        }

        Destroy(gameObject, destroyDelay);
    }
}