using System.Collections;
using UnityEngine;

public abstract class BaseGuardianAbilities : MonoBehaviour 
{
    [Header("Ability")]
    public GameObject ability;

    [Header("Damage Attributes")]
    public DamageType damageType;
    public float damageAmount;

    [Header("Range Attributes")]
    public float projectileSpeed;
    public float spawnDistanceFromPlayer;
    public float activeTime;

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

    [Header("Has Effect")]
    public bool canStun;
    public bool canSlow;
    public bool canDrainHealth;
    public bool canDrainMana;
    public bool canBlind;

    [Header("Ability Info")]
    public AbilityValues abilityValues;

    public virtual void CastAbility(Vector3 aimVec, GameObject spawnPos, PlayerTeam teamColor)
    {
        Debug.LogError("Base Ability Cast, should have been overriden");
    }
}
