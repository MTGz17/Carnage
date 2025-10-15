using UnityEngine;

public class CurrencyAdd : MonoBehaviour
{
    [SerializeField] private int pointsOnDestroy = 10;
    [SerializeField] private AudioClip cashSound;

    private bool hasTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered || !other.CompareTag("Player")) return;
        hasTriggered = true;

        CurrencyManager.Instance?.AddCurrency(pointsOnDestroy);

        if (cashSound)
            AudioSource.PlayClipAtPoint(cashSound, transform.position);

        GetComponent<Renderer>().enabled = false;

        Destroy(gameObject, cashSound ? cashSound.length : 0f);
    }
}