using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private AudioSource musicSource;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        musicSource.Pause();
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        musicSource.UnPause();
    }

    public void ToMainMenu()
    {
        Time.timeScale = 1;
        musicSource.Stop();
        SceneManager.LoadSceneAsync(1);
    }
}