using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FrameCounter : MonoBehaviour
{
    private Text frameDisplayText;

    private float frameRate;

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        GetFramerate();
        DisplayFramerate();
    }

    void Initialize()
    {
        frameDisplayText = GetComponent<Text>();
    }

    void GetFramerate()
    {
        frameRate = (int) (1.0f/Time.smoothDeltaTime);
    }

    void DisplayFramerate()
    {
        frameDisplayText.text = frameRate.ToString() + " FPS";
    }
}
