using System.Collections;
using UnityEngine;

public class AbilityCastBase : MonoBehaviour 
{
    private TimerClass abilityTimer;

    public AbilityValues abilityValues;

    public bool isAbilityActive = false;

    [Header("Audio Attributes")]
    public AudioSource castAudio;
    public AudioSource collisionAudio;

	public virtual void InitializeAbility()
    {

    }
}
