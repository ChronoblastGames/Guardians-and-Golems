using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Settings")]
    private static AudioManager audioManagerInstance;

    private AudioSource currentAudioSource;

    private float audioVolume;
    private float lerpTime = 1f;
    private float t;

    private bool canLerpInClip;
    private bool canLerpOutClip;

    private void Start()
    {
        InstanceManagement();

        currentAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        ManageClips();
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

    private void ManageClips()
    {
        if (canLerpInClip)
        {
            audioVolume = currentAudioSource.volume;

            currentAudioSource.volume = Mathf.Lerp(currentAudioSource.volume, 1, t);

            t += (Time.deltaTime / lerpTime);

            if (currentAudioSource.volume == 1)
            {
                t = 0;
                canLerpInClip = false;
            }
        }
        else if (canLerpOutClip)
        {
            currentAudioSource.volume = Mathf.Lerp(currentAudioSource.volume, 0, t);

            t += (Time.deltaTime / lerpTime);

            if (currentAudioSource.volume == 0)
            {
                t = 0;
                canLerpOutClip = false;
            }
        }
    }

    public void PhaseInClip(AudioClip queuedClip)
    {
        currentAudioSource.clip = queuedClip;

        canLerpInClip = true;
    }
}
