using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1;
    }

    public void LoadPreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Time.timeScale = 1;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void LoadLobby()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

}
