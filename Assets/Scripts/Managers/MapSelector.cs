using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelector : MonoBehaviour
{
    public void LoadSanFran()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void LoadVegas()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
