using UnityEngine;

public class ScoreMult : MonoBehaviour
{
    [SerializeField] private int multiplier = 2;
    [SerializeField] private float duration = 15f;
    [SerializeField] private AudioClip scoreMultiplierSound;

    private bool hasTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered || !other.CompareTag("Player")) return;
        hasTriggered = true;

        ScoreManager.Instance?.ActivateMultiplier(multiplier, duration);

        if (ScreenAnimator.Instance != null)
        {
            ScreenAnimator.Instance.PlayDoubleAnimation();
        }

        if (scoreMultiplierSound)
            AudioSource.PlayClipAtPoint(scoreMultiplierSound, transform.position);

        GetComponent<Renderer>().enabled = false;

        Destroy(gameObject, scoreMultiplierSound ? scoreMultiplierSound.length : 0f);
    }
}