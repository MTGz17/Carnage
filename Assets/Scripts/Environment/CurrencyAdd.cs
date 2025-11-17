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

        SaveManager.instance?.AddCurrency(pointsOnDestroy);

        if (cashSound)
            AudioSource.PlayClipAtPoint(cashSound, transform.position);

        if (ScreenAnimator.Instance != null)
        {
            ScreenAnimator.Instance.PlayCashAnimation();
        }

        GetComponent<Renderer>().enabled = false;

        Destroy(gameObject, cashSound ? cashSound.length : 0f);
    }
}