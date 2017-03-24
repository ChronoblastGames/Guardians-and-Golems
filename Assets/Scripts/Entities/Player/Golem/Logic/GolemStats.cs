using System.Collections;
using UnityEngine;

public abstract class GolemStats : MonoBehaviour 
{
    [Header("Golem Movement Attributes")]
    public float baseMovementSpeed;

    public float dodgeDistance;
    [HideInInspector]
    public float dodgeSpeed;

    [Header("Golem Offenses")]
    public GolemOffense golemOffensiveValues;

    [Header("Golem Defenses")]
    public GolemDefense golemDefenseValues;

    [Header("Golem Abilities")]
    public CreateAbilityBase[] golemAbilities;
}

[System.Serializable]
public struct GolemDefense
{
    [Header("Golem Physical Defense Attributes")]
    public float baseDefense;
    public float slashDefense;
    public float smashDefense;
    public float pierceDefense;

    [Header("Golem Magic Defense Attributes")]
    public float fireDefense;
    public float waterDefense;
    public float earthDefense;
    public float windDefense;

    [Header("Golem Stability")]
    public float golemStability;

    public float golemStaggerTime; //How long the Golem is staggered for
}

[System.Serializable]
public struct GolemOffense
{
    [Header("Golem Physical Offensive Attributes")]
    public float baseAttack;
    public float slashAttack;
    public float smashAttack;
    public float pierceAttack;

    [Header("Golem Magical Offensive Attributes")]
    public float fireAttack;
    public float waterAttack;
    public float earthAttack;
    public float windAttack;
}
