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

    [Header("Golem Health Attributes")]
    public float maxHealth;

    [Header("Golem Mana/Energy Attributes")]
    public float maxMana;
}
