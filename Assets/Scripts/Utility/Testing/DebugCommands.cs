using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugCommands : MonoBehaviour
{
    private MapManager mapManager;

    [Header("Conduits")]
    private GameObject[] conduitArray;

    [Header("UI")]
    public GameObject[] objectsToDisable;

    private void Start()
    {
        Initialize();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetConduits();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            DisableUI();
        }
    }

    void Initialize()
    {
        mapManager = GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>();

        conduitArray = mapManager.conduitArray;
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
