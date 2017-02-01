using System.Collections;
using UnityEngine;

public abstract class GolemStats : MonoBehaviour 
{
    [Header("Golem Offensive Stats")]
    public float baseStrength;
    public float baseDexterity;

    [Header("Golem Defensive Stats")]
    public float baseDefense;
    public float baseStability;

    [Header("Golem Movement Attributes")]
    public float baseMovementSpeed;

    [Header("Golem Dodge Attributes")]
    public float dodgeStrength;

    [Header("Golem Health Attributes")]
    public float baseHealth;

    [Header("Golem Mana/Energy Attributes")]
    public float baseMana;

    [Header("Golem Abilities")]
    public GolemAbility[] golemAbilities;
}
