using System.Collections;
using UnityEngine;

public enum DamageType
{
    FIRE,
    ICE,
    WIND,
    EARTH,
    SLASH,
    SMASH,
    PIERCE,
    NONE
}

public enum AbilityType
{
    PROJECTILE,
    STATIC,
    ZONE,
    BUFF,
    DEBUFF,
}

public struct AbilityValues
{
    [Header("Damage Attributes")]
    public DamageType damageType;
    public float damageAmount;

    [Header("Range Attributes")]
    public float projectileSpeed;
    public float spawnDistanceFromPlayer;
    public float rangeDistance;

    public bool isMelee;
    public bool isRanged;

    [Header("Use Attributes")]
    public float healthCost;
    public float manaCost;

    [Header("Has Effect")]
    public bool canStun;
    public bool canSlow;
    public bool canDrainHealth;
    public bool canDrainMana;
    public bool canBlind;
}

public abstract class GolemAbility : MonoBehaviour
{
    [Header("Ability")]
    public GameObject ability;

    [Header("Damage Attributes")]
    public DamageType damageType;
    public float damageAmount;

    [Header("Range Attributes")]
    public float projectileSpeed;
    public float spawnDistanceFromPlayer;
    public float rangeDistance;

    public bool isMelee;
    public bool isRanged;

    [Header("Use Attributes")]
    public float healthCost;
    public float manaCost;

    [Header("Has Effect")]
    public bool canStun;
    public bool canSlow;
    public bool canDrainHealth;
    public bool canDrainMana;
    public bool canBlind;

    [Header("Ability Info")]
    public AbilityValues abilityValues;

    public virtual void CastAbility(Vector3 aimVec, string teamColor)
    {
        Debug.LogError("Base Ability Cast, should have been overriden");
    }
}


