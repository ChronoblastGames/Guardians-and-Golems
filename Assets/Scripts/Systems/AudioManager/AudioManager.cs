using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Settings")]
    private static AudioManager audioManagerInstance;

    private AudioSource currentAudioSource;

    private void Start()
    {
        InstanceManagement();

        currentAudioSource = GetComponent<AudioSource>();
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

    public void ChangeClip(AudioClip queuedClip)
    {
        currentAudioSource.Pause();

        currentAudioSource.clip = queuedClip;

        currentAudioSource.Play();
    }
}
