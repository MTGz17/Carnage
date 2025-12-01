using UnityEngine;

public class MediumObject : MonoBehaviour
{
    private Rigidbody rb;
    public float launchForce = 8f;
    public float upwardForce = 10f;
    public int pointsOnDestroy = 50;

    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        float destroyDelay = 0f;
        if (audioSource != null)
        {
            audioSource.Play();
            destroyDelay = audioSource.clip.length;
        }

        Vector3 impactDirection = collision.relativeVelocity.normalized;
        Vector3 launchDirection = impactDirection + Vector3.up * upwardForce;
        rb.AddForce(launchDirection * launchForce, ForceMode.Impulse);

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