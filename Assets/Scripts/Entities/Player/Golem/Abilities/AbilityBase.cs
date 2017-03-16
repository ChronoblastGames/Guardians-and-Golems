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
    SELF,
}

public struct AbilityValues
{
    [Header("Ability Cast Time")]
    public float abilityCastTime;

    [Header("Damage Attributes")]
    public DamageType damageType;
    public float damageAmount;

    [Header("Range Attributes")]
    public float projectileSpeed;
    public float spawnDistanceFromPlayer;
    public float activeTime;
    public float holdTime;

    [Header("Wall Attributes")]
    public float raiseAmount;
    public float raiseSpeed;

    [Header("Zone Attributes")]
    public float zoneRadius;
    public float zoneHeight;
    public float zoneStrength;

    [Header("Use Attributes")]
    public float healthCost;
    public float manaCost;

    public bool isMelee;
    public bool isRanged;
    public bool isHeld;

    [Header("Ability Effect")]
    public StatusEffect statusEffect;

    public float effectStrength;
    public float effectTime;
}

public abstract class AbilityBase : MonoBehaviour
{
    [Header("Ability")]
    public GameObject ability;

    [Header("Ability Cast Time")]
    public float abilityCastTime;

    [Header("Damage Attributes")]
    public DamageType damageType;
    public float damageAmount;

    [Header("Range Attributes")]
    public float projectileSpeed;
    public float spawnDistanceFromPlayer;
    public float activeTime;
    public float holdTime;

    [Header("Wall Attributes")]
    public float raiseAmount;
    public float raiseSpeed;

    [Header("Zone Attributes")]
    public float zoneRadius;
    public float zoneHeight;
    public float zoneStrength;

    [Header("Use Attributes")]
    public float healthCost;
    public float manaCost;

    public bool isMelee;
    public bool isRanged;
    public bool isHeld;

    [Header("Ability Effect")]
    public StatusEffect statusEffect;

    public float effectStrength;
    public float effectTime;

    [Header("Ability Info")]
    public AbilityValues abilityValues;

    public virtual void CastAbility(Vector3 aimVec, PlayerTeam teamColor, float holdTime)
    {
        Debug.LogError("Base Ability Cast, should have been overriden");
    }

    public virtual void CastGuardianAbility(Vector3 aimVec, GameObject spawnPos, PlayerTeam color, float holdTime)
    {
        Debug.LogError("Base GuardianAbility Cast, should have been overriden");
    }

    public virtual void InitializeAbility()
    {
        Debug.LogError("Base Ability Initialize, should have been overriden");
    }
}


