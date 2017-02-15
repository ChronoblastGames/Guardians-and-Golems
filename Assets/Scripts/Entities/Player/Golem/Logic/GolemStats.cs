using System.Collections;
using UnityEngine;

public abstract class GolemStats : MonoBehaviour 
{
    [Header("Golem Movement Attributes")]
    public float baseMovementSpeed;
    public float dodgeStrength;

    [Header("Player Turning Attributes")]
    public float turnSpeed;

    [Header("Golem Defenses")]
    public GolemDefense golemDefenseValues;

    [Header("Golem Abilities")]
    public BaseGolemAbilities[] golemAbilities;
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
}
