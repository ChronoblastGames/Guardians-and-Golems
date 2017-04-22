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
    public GameObject elipsesHolder;

    [Header("Sprites")]
    public GameObject wireframeSprite;
    public GameObject fullSprite;

    private bool isReadyToGo = false;
    private bool isActivated = false;

    [Header("Lerp Attributes")]
    public float firstLerpTime;
    public float secondLerpTime;

    private float t = 0f;
    private float o = 0f;

    public GameObject firstPoint;
    public GameObject secondPoint;
    public GameObject thirdPoint;

    private GameObject movingObject = null;

    private bool firstLerp = false;
    private bool secondLerp = false;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    private void Update()
    {
        CheckForInput();
        LerpSprites();
    }

    private IEnumerator LoadScene ()
    {
        movingObject = wireframeSprite;

        firstLerp = true;

        asyncLoader = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        asyncLoader.allowSceneActivation = false;

        while (!asyncLoader.isDone)
        {
            yield return new WaitForSeconds(loadingTransitionTime);

            SwitchSprites();

            yield return new WaitForSeconds(loadDelay);

            loadingText.text = "Press any Button";
            elipsesHolder.SetActive(false);

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

    private void LerpSprites()
    {
        if (firstLerp)
        {
            movingObject.transform.position = Vector3.Lerp(firstPoint.transform.position, secondPoint.transform.position, t);

            t += Time.deltaTime / firstLerpTime;
        }
        else if (secondLerp)
        {
            movingObject.transform.position = Vector3.Lerp(secondPoint.transform.position, thirdPoint.transform.position, o);

            o += Time.deltaTime / secondLerpTime;
        }
    }

    private void SwitchSprites()
    {
        if (!isActivated)
        {
            wireframeSprite.SetActive(false);
            fullSprite.SetActive(true);

            movingObject = fullSprite;

            firstLerp = false;
            secondLerp = true;

            isActivated = true;
        }
    }
}
