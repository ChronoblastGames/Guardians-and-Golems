using System.Collections;
using UnityEngine;

public class AudioQueuer : MonoBehaviour 
{
    private AudioManager audioManager;

    [Header("Audio Settings")]
    public AudioSource queuedClip;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }     
}
