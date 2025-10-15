using UnityEngine;

public class CurrencyAdd : MonoBehaviour
{
    [SerializeField] private int pointsOnDestroy = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (CurrencyManager.Instance != null)
            {
                CurrencyManager.Instance.AddCurrency(pointsOnDestroy);
            }
        }
        Destroy(gameObject);
    }
}