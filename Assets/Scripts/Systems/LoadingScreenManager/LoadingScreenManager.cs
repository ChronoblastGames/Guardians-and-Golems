using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenManager : MonoBehaviour
{
    private AsyncOperation asyncLoader;

    [Header("Loading Attributes")]
    public int sceneID;

    public float loadDelay;
    public float loadingTransitionTime;

    [Header("UI Attributes")]
    public Text loadingText;

    [Header("Sprites")]
    public GameObject wireframeSprite;
    public GameObject fullSprite;

    private bool isReadyToGo = false;

    private bool isActivated = false;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    private void Update()
    {
        CheckForInput();
    }

    private IEnumerator LoadScene ()
    {
        asyncLoader = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        asyncLoader.allowSceneActivation = false;

        while (!asyncLoader.isDone)
        {
            yield return new WaitForSeconds(loadingTransitionTime);

            SwitchSprites();

            yield return new WaitForSeconds(loadDelay);

            isReadyToGo = true;

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
