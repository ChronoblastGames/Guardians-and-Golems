using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Settings")]
    private static AudioManager audioManagerInstance;

    private void Start()
    {
        InstanceManagement();
    }

    private void InstanceManagement()
    {
        if (audioManagerInstance != null && audioManagerInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            audioManagerInstance = this;

            DontDestroyOnLoad(gameObject);
        }
    }
}
