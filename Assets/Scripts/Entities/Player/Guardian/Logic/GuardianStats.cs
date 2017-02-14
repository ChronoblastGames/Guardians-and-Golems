using System.Collections;
using UnityEngine;

public abstract class GuardianStats : MonoBehaviour 
{
    [Header("Guardian Capture Speed")]
    public float captureSpeed;

    [Header("Guardian Abilities")]
    public GuardianAbility[] guardianAbilites;
}
