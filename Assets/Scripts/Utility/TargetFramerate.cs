using System.Collections;
using UnityEngine;

public class TargetFramerate : MonoBehaviour
{
    public int targetFrameRate;

    private void Start()
    {
        Application.targetFrameRate = targetFrameRate;
    }
}
