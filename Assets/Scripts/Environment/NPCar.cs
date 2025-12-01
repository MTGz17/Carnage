using UnityEngine;

public class NPCar : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;
    public float launchForce = 8f;
    public float upwardForce = 10f;
    public int pointsOnDestroy = 500;

    private FinalManager finalManager;
    private AudioSource audioSource;
    private bool hasPlayedSound = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        finalManager = FindFirstObjectByType<FinalManager>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        float destroyDelay = 0f;
        if (!hasPlayedSound && audioSource != null)
        {
            audioSource.Play();
            hasPlayedSound = true;
            destroyDelay = audioSource.clip.length;
        }

        Vector3 impactDirection = collision.relativeVelocity.normalized;
        Vector3 launchDirection = impactDirection + Vector3.up * upwardForce;
        rb.AddForce(launchDirection * launchForce, ForceMode.Impulse);

        float impactSpeed = collision.relativeVelocity.magnitude;

        if (impactSpeed >= 13.41f)
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddPoints(pointsOnDestroy);
            }

            Destroy(gameObject, destroyDelay > 0f ? destroyDelay : 1f);
        }
        else
        {
            if (finalManager != null)
            {
                finalManager.ShowFinalScore();
            }
            else
            {
                Debug.LogWarning("FinalManager not found in scene!");
            }
        }
    }
}