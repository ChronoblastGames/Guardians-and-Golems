using System.Collections;
using UnityEngine;

public abstract class GuardianStats : MonoBehaviour 
{
    [Header("Guardian Capture Speed")]
    public float captureSpeed;
    public float assistedCaptureSpeed;

    [Header("Guardian Abilities")]
    public CreateAbilityBase[] guardianAbilites;
}

[System.Serializable]
public struct GuardianOffense
{
    [Header("Guardian Physical Damage Attributes")]
    public float baseDamageBoost;
    public float slashDamageBoost;
    public float smashDamageBoost;
    public float pierceDamageBoost;

    [Header("Guardian Magic Damage Attributes")]
    public float fireDamageBoost;
    public float waterDamageBoost;
    public float earthDamageBoost;
    public float windDamageBoost;
}
