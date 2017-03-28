using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadOnPress : MonoBehaviour
{
    [Header("UI")]
    public GameObject[] objectsToDisable;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReloadScene();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            DisableUI();
        }
    }

    void DisableUI()
    {
        foreach (GameObject objectUI in objectsToDisable)
        {
            objectUI.SetActive(false);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }	
}
