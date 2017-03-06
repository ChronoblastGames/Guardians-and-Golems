using System.Collections;
using UnityEngine;

public abstract class GuardianStats : MonoBehaviour 
{
    [Header("Guardian Capture Speed")]
    public float captureSpeed;

    [Header("Guardian Abilities")]
    public AbilityBase[] guardianAbilites;

    public bool canAttack = false;
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
