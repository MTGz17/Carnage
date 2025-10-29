using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    [SerializeField] private AudioClip speedPowerSound;

    private bool hasTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered || !other.CompareTag("Player")) return;
        hasTriggered = true;

        other.GetComponent<PlayerCarController>()?.BoostSpeed();

        if (speedPowerSound)
            AudioSource.PlayClipAtPoint(speedPowerSound, transform.position);

        GetComponent<Renderer>().enabled = false;

        Destroy(gameObject, speedPowerSound ? speedPowerSound.length : 0f);
    }
}