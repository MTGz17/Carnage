using UnityEngine;

public class ScoreMult : MonoBehaviour
{
    [SerializeField] private int multiplier = 2;
    [SerializeField] private float duration = 15f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.ActivateMultiplier(multiplier, duration);
            }

            Destroy(gameObject);
        }
    }
}