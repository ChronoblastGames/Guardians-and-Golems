using System.Collections;
using UnityEngine;

public abstract class GuardianStats : MonoBehaviour 
{
    [Header("Guardian Movement Attributes")]
    public float baseMovementSpeed;

    [Header("Player Turning Attributes")]
    public float turnSpeed;

    [Header("Guardian Abilities")]
    public GuardianAbility[] guardianAbilites;
}
