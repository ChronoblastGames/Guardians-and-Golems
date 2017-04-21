using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class SplashScreenManager : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        float videoLength = (float)videoPlayer.clip.length;

        StartCoroutine(GoToNextScene(videoLength));
    }

    private IEnumerator GoToNextScene(float waitTime)
    {
        if (waitTime > 0)
        {
            yield return new WaitForSeconds(waitTime);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
