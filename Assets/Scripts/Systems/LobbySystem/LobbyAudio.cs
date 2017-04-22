using System.Collections;
using UnityEngine;

public class LobbyAudio : MonoBehaviour
{
    [Header("Audio Attributes")]
    public AudioSource readyUpSFX;

    public void PlayReadySound()
    {
        readyUpSFX.Play();
    }
}
