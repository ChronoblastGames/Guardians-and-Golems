using System.Collections;
using UnityEngine;

public struct AbilityValues
{
    [Header("Cast Info")]
    public AbilityType abilityType;
    [Space(10)]
    public IndicatorType indicatorType;
    public float indicatorSize;
    [Space(10)]
    public GameObject createPoint;
    [HideInInspector]
    public GameObject casterGameObject;
    [HideInInspector]
    public GameObject casterConduit;
    [HideInInspector]
    public PlayerTeam teamColor;

    [Header("Ability Cast Time")]
    public float abilityCastTime;

    [Header("Damage Attributes")]
    public DamageType damageType;
    public float damageAmount;
    public float damageFrequency;

    [Header("Range Attributes")]
    public float travelSpeed;
    public float spawnDistanceFromPlayer;
    public float activeTime;
    public float holdTime;

    [Header("Zone Attributes")]
    public float zoneRadius;
    public float zoneHeight;

    [Header("Use Attributes")]
    public float healthCost;
    public int crystalCost;

    public bool isMelee;
    public bool isRanged;
    public bool isHeld;

    [Header("Ability Effect")]
    public StatusEffect statusEffect;

    public float effectStrength;
    public float effectTime;
    public float effectFrequency;
}

public abstract class CreateAbilityBase : MonoBehaviour
{
    [Header("Cast Info")]
    public AbilityType abilityType;
    [Space(10)]
    public IndicatorType indicatorType;
    public float indicatorSize;
    [Space(10)]
    public GameObject spawnPos;
    [HideInInspector]
    public GameObject casterGameObject;
    [HideInInspector]
    public GameObject casterConduit;
    [HideInInspector]
    public PlayerTeam teamColor;

    [Header("Ability")]
    public GameObject ability;

    [Header("Ability Cast Time")]
    public float abilityCastTime;

    [Header("Damage Attributes")]
    public DamageType damageType;
    public float damageAmount;
    public float damageFrequency;

    [Header("Range Attributes")]
    public float travelSpeed;
    public float spawnDistanceFromPlayer;
    public float activeTime;
    public float holdTime;

    [Header("Zone Attributes")]
    public float zoneRadius;
    public float zoneHeight;

    [Header("Use Attributes")]
    public float healthCost;
    public int crystalCost;

    public bool isMelee;
    public bool isRanged;
    public bool isHeld;

    [Header("Ability Effect")]
    public StatusEffect statusEffect;

    public float effectStrength;
    public float effectTime;
    public float effectFrequency;

    [Header("Ability Info")]
    public AbilityValues abilityValues;

    public virtual void CastAbility(PlayerTeam teamColor, float holdTime, GameObject casterObject)
    {
        Debug.LogError("Base Golem Ability Cast, should have been overriden");
    }

    public virtual void CastGuardianAbility(PlayerTeam color, float holdTime, GameObject spawnPos, GameObject casterObject, GameObject casterConduit)
    {
        Debug.LogError("Base Guardian Ability Cast, should have been overriden");
    }

    public virtual void InitializeAbility()
    {
        Debug.LogError("Base Ability Initialize, should have been overriden");
    }
}


