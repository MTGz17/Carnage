using UnityEngine;

public class MediumObject : MonoBehaviour
{
    private Rigidbody rb;
    private AudioSource audioSource;

    public float launchForce = 8f;
    public float upwardForce = 10f;
    public int pointsOnDestroy = 50;
    public float destroyDelay = 1.5f;

    private bool isDestroying = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        if (audioSource != null)
        {
            audioSource.Play();
        }

        Vector3 impactDirection = collision.relativeVelocity.normalized;
        Vector3 launchDirection = impactDirection + Vector3.up * upwardForce;
        rb.AddForce(launchDirection * launchForce, ForceMode.Impulse);

        if (isDestroying) return;

        if (collision.relativeVelocity.magnitude >= 11.17f)
        {
            isDestroying = true;

            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddPoints(pointsOnDestroy);
            }

            Destroy(gameObject, destroyDelay);
        }
    }
}