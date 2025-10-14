using UnityEngine;

public class ScoreMult : MonoBehaviour
{
    [SerializeField] private int multiplier = 2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.score *= multiplier;

                ScoreManager.Instance.UpdateScoreUI();
            }
            
            Destroy(gameObject);
        }
    }
}