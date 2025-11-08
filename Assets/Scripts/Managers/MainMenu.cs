using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void Shop()
    {
        SceneManager.LoadSceneAsync(3);
    }
}