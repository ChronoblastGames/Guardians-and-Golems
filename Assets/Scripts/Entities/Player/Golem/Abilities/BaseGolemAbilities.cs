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
    PURE,
    NONE
}

public enum AbilityType
{
    PROJECTILE,
    STATIC,
    ZONE,
    BEAM,
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

    [Header("Projectile Attributes")]
    public float projectileActiveTime;

    [Header("Wall Attributes")]
    public float wallActiveTime;
    public float raiseAmount;
    public float raiseSpeed;

    [Header("Zone Attributes")]
    public float zoneRadius;
    public float zoneHeight;
    public float zoneStrength;
    public float zoneTime;

    [Header("Use Attributes")]
    public float healthCost;
    public float manaCost;

    public bool isMelee;
    public bool isRanged;

    [Header("Has Effect")]
    public bool canStun;
    public bool canSlow;
    public bool canDrainHealth;
    public bool canDrainMana;
    public bool canBlind;
}

public abstract class BaseGolemAbilities : MonoBehaviour
{
    [Header("Ability")]
    public GameObject ability;

    [Header("Damage Attributes")]
    public DamageType damageType;
    public float damageAmount;

    [Header("Range Attributes")]
    public float projectileSpeed;
    public float spawnDistanceFromPlayer;

    [Header("Projectile Attributes")]
    public float projectileActiveTime;

    [Header("Wall Attributes")]
    public float wallActiveTime;
    public float raiseAmount;
    public float raiseSpeed;

    [Header("Zone Attributes")]
    public float zoneRadius;
    public float zoneHeight;
    public float zoneStrength;
    public float zoneTime;

    [Header("Use Attributes")]
    public float healthCost;
    public float manaCost;

    public bool isMelee;
    public bool isRanged;

    [Header("Has Effect")]
    public bool canStun;
    public bool canSlow;
    public bool canDrainHealth;
    public bool canDrainMana;
    public bool canBlind;

    [Header("Ability Info")]
    public AbilityValues abilityValues;

    public virtual void CastAbility(Vector3 aimVec, PlayerTeam teamColor)
    {
        Debug.LogError("Base Ability Cast, should have been overriden");
    }

    public virtual void InitializeAbility()
    {
        Debug.LogError("Base Ability Initialize, should have been overriden");
    }
}


