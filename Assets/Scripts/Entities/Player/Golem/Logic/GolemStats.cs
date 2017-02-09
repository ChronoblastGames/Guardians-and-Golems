using System.Collections;
using UnityEngine;

public abstract class GolemStats : MonoBehaviour 
{
    [Header("Golem Movement Attributes")]
    public float baseMovementSpeed;

    [Header("Golem Dodge Attributes")]
    public float dodgeStrength;

    [Header("Golem Health Attributes")]
    public float baseHealth;

    [Header("Golem Mana/Energy Attributes")]
    public float baseMana;

    [Header("Golem Defenses")]
    public GolemDefense golemDefenseValues;

    [Header("Golem Abilities")]
    public GolemAbility[] golemAbilities;
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
