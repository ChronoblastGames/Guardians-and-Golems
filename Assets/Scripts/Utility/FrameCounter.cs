using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FrameCounter : MonoBehaviour
{
    private Text frameCountText;

    private float deltaTime = 0f;
    private float framesPerSecond;

    void Start()
    {
        frameCountText = GetComponent<Text>();
    }

    void Update()
    {
        FrameCount();
    }

    void FrameCount()
    {
        deltaTime += Time.deltaTime;

        deltaTime /= 2.0f;

        framesPerSecond = 1.0f / deltaTime;

        framesPerSecond = Mathf.FloorToInt(framesPerSecond);

        frameCountText.text = framesPerSecond + " FPS";
    }

}
