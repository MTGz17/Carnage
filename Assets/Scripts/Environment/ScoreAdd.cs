using UnityEngine;

public class ScoreAdd : MonoBehaviour
{
    [SerializeField] private int pointsOnDestroy = 10000;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddPoints(pointsOnDestroy);
            }
        }
        Destroy(gameObject);
    }
}