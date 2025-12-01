using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class FinalManager : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public GameObject gameScreen;
    public GameObject finalScorePanel;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI cashText;

    [Header("Transfer Settings")]
    public int scoreToCashRatio = 1000;
    public float transferSpeed = 1000f;

    private Coroutine transferCoroutine;

    public void ShowFinalScore()
    {
        gameScreen?.SetActive(false);

        finalScorePanel?.SetActive(true);

        finalScoreText.text = ScoreManager.Instance?.score.ToString() ?? "0";
        cashText.text = SaveManager.instance?.currency.ToString() ?? "0";

        if (transferCoroutine != null)
            StopCoroutine(transferCoroutine);

        transferCoroutine = StartCoroutine(TransferScoreToCash());
    }

    private IEnumerator TransferScoreToCash()
    {
        if (ScoreManager.Instance == null || SaveManager.instance == null)
            yield break;

        float startScore = ScoreManager.Instance.score;
        float startCash = SaveManager.instance.currency;

        float duration = 4f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            float currentScore = Mathf.Lerp(startScore, 0, t);

            float targetCash = startCash + (startScore - currentScore) / scoreToCashRatio;

            finalScoreText.text = Mathf.CeilToInt(currentScore).ToString();
            cashText.text = Mathf.FloorToInt(targetCash).ToString();

            yield return null;
        }

        ScoreManager.Instance.score = 0;
        int finalCash = Mathf.FloorToInt(startCash) + Mathf.FloorToInt(startScore / scoreToCashRatio);
        SaveManager.instance.SetCurrency(finalCash);

        finalScoreText.text = "0";
        cashText.text = finalCash.ToString();
    }

    public void PlayAgain()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void MainMenuReturn()
    {
        SceneManager.LoadSceneAsync(1);
    }
}