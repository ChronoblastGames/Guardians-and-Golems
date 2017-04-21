using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenManager : MonoBehaviour
{
    private AsyncOperation asyncLoader;

    [Header("Loading Attributes")]
    public int sceneID;

    public float loadDelay;
    public float loadingTransitionTime;

    [Header("Sprites")]
    public GameObject wireframeSprite;
    public GameObject fullSprite;

    private bool isReadyToGo = false;

    private bool isActivated = false;

    private void Start()
    {
        StartCoroutine(LoadScene(sceneID));
    }

    private void Update()
    {
        CheckForInput();
    }

    private IEnumerator LoadScene (int sceneIndex)
    {
        Debug.Log("Starting Load");

        asyncLoader = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
        asyncLoader.allowSceneActivation = false;

        while (!asyncLoader.isDone)
        {
            yield return new WaitForSeconds(loadingTransitionTime);

            SwitchSprites();

            yield return new WaitForSeconds(loadDelay);

            isReadyToGo = true;

            Debug.Log("Ready to Go!");

            yield return null;
        }
    }

    private void CheckForInput()
    {
        if (isReadyToGo)
        {
            if (Input.anyKeyDown)
            {
                asyncLoader.allowSceneActivation = true;
            }
        }
    }

    private void SwitchSprites()
    {
        if (!isActivated)
        {
            wireframeSprite.SetActive(false);
            fullSprite.SetActive(true);
            isActivated = true;
        }
    }
}
