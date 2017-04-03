using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugCommands : MonoBehaviour
{
    [Header("Conduits")]
    public GameObject[] conduitArray;

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
    
    void ResetConduits()
    {
        foreach (GameObject conduit in conduitArray)
        {
            conduit.GetComponent<ConduitController>().ResetConduit();
        }
    }
}
