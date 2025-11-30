using UnityEngine;

public class NPCar : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;
    public float launchForce = 8f;
    public int pointsOnDestroy = 500;
    public float destroyDelay = 1f;

    private FinalManager finalManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        finalManager = FindObjectOfType<FinalManager>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        Vector3 impactDirection = collision.relativeVelocity.normalized;
        rb.AddForce(impactDirection * launchForce, ForceMode.Impulse);

        float impactSpeed = collision.relativeVelocity.magnitude;

        if (impactSpeed >= 13.41f)
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddPoints(pointsOnDestroy);
            }
            Destroy(gameObject, destroyDelay);
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