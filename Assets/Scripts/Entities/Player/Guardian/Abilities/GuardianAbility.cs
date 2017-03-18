﻿using System.Collections;
using UnityEngine;

public class GuardianAbility : AbilityBase
{
    private GuardianResources guardianResources;

    [Header("Ability Type")]
    public AbilityType abilityType;

    [Header("Ability Mask")]
    public LayerMask redMask;
    public LayerMask blueMask;

    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        guardianResources = transform.parent.parent.GetComponent<GuardianResources>();
    }

    public override void CastGuardianAbility(Vector3 aimVec, GameObject spawnPos, PlayerTeam teamColor, float holdTime)
    {

    }

    public AbilityValues CreateAbilityStruct()
    {
        AbilityValues abilityInfo;

        abilityInfo.casterGameObject = casterGameObject;
        abilityInfo.abilityCastTime = abilityCastTime;
        abilityInfo.damageType = damageType;
        abilityInfo.damageAmount = damageAmount;
        abilityInfo.activeTime = activeTime;
        abilityInfo.holdTime = holdTime;
        abilityInfo.projectileSpeed = projectileSpeed;
        abilityInfo.spawnDistanceFromPlayer = spawnDistanceFromPlayer;
        abilityInfo.raiseAmount = raiseAmount;
        abilityInfo.raiseSpeed = raiseSpeed;
        abilityInfo.zoneRadius = zoneRadius;
        abilityInfo.zoneHeight = zoneHeight;
        abilityInfo.zoneStrength = zoneStrength;
        abilityInfo.isMelee = isMelee;
        abilityInfo.isRanged = isRanged;
        abilityInfo.isHeld = isHeld;
        abilityInfo.healthCost = healthCost;
        abilityInfo.manaCost = manaCost;
        abilityInfo.statusEffect = statusEffect;
        abilityInfo.effectStrength = effectStrength;
        abilityInfo.effectTime = effectTime;

        return abilityInfo;
    }
}
