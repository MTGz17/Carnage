using UnityEngine;

public class FinalPoints : MonoBehaviour
{
    public int finalPoints = 10000;
    public AudioSource audioSource;

    private bool isTriggered = false;
    private FinalManager finalManager;

    private void Start()
    {
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas != null)
        {
            Transform managerTransform = canvas.transform.Find("Manager");
            if (managerTransform != null)
                finalManager = managerTransform.GetComponent<FinalManager>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isTriggered || !other.CompareTag("Player")) return;
        isTriggered = true;

        ScoreManager.Instance?.AddPoints(finalPoints);

        StartCoroutine(PlayAudioThenShowScore());
    }

    private System.Collections.IEnumerator PlayAudioThenShowScore()
    {
        if (audioSource != null)
        {
            audioSource.Play();

            while (audioSource.isPlaying)
                yield return null;
        }

        finalManager?.ShowFinalScore();
    }
}