using System.Collections;
using UnityEngine;

public class GolemAbility : AbilityBase 
{
    private GolemResources golemResources;

    [Header("Ability Type")]
    public AbilityType abilityType;

    [Header("Ability Mask")]
    public LayerMask redMask;
    public LayerMask blueMask;

    [Header("Ability Debug")]
    public float minHoldTime = 0.25f;

    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        golemResources = transform.parent.parent.GetComponent<GolemResources>();
    }

    public override void CastAbility(Vector3 aimVec, PlayerTeam teamColor, float heldTime)
    {
        AbilityValues abilityValues;
        Vector3 spawnVec;
        Quaternion spawnRot;

        abilityValues = CreateAbilityStruct();

        if (heldTime < minHoldTime)
        {
            holdTime = 0;
        }

        holdTime = heldTime;

        switch(abilityType)
        {
            case AbilityType.SELF:
                break;

            case AbilityType.PROJECTILE:

                spawnRot = Quaternion.LookRotation(aimVec);

                spawnVec = transform.position + new Vector3(0, 1, 0) + new Vector3(0, 0, abilityValues.spawnDistanceFromPlayer);

                StartCoroutine(FireAbility(ability, spawnVec, spawnRot, abilityValues.abilityCastTime, teamColor, null, abilityValues));

                break;

            case AbilityType.STATIC:

                spawnRot = Quaternion.LookRotation(aimVec);

                spawnVec = aimVec;
                spawnVec.Normalize();
                spawnVec = spawnVec * abilityValues.spawnDistanceFromPlayer;

                spawnVec = transform.position + spawnVec;

                StartCoroutine(FireAbility(ability, spawnVec, spawnRot, abilityValues.abilityCastTime, teamColor, null, abilityValues));

                break;

            case AbilityType.ZONE:

                spawnRot = Quaternion.LookRotation(aimVec);

                Vector3 zoneSpawnVec = aimVec;
                zoneSpawnVec.Normalize();
                zoneSpawnVec = zoneSpawnVec * abilityValues.spawnDistanceFromPlayer;
                zoneSpawnVec.y = 0f;

                zoneSpawnVec = transform.position + zoneSpawnVec;

                StartCoroutine(FireAbility(ability, zoneSpawnVec, spawnRot, abilityValues.abilityCastTime, teamColor, null, abilityValues));

                break;
        }       
    }

    private IEnumerator FireAbility(GameObject ability, Vector3 spawnPos, Quaternion spawnRot, float castTime, PlayerTeam teamColor, GameObject target, AbilityValues abilityInfo)
    {
        yield return new WaitForSeconds(castTime);

        if (teamColor == PlayerTeam.RED)
        {
            ability.layer = 8;
        }
        else if (teamColor == PlayerTeam.BLUE)
        {
            ability.layer = 9;
        }
        GameObject newAbility = Instantiate(ability, spawnPos, spawnRot) as GameObject;

        newAbility.GetComponent<GolemAbilityBase>().abilityValues = abilityInfo;
        newAbility.GetComponent<GolemAbilityBase>().InitializeAbility();
    }

    public AbilityValues CreateAbilityStruct()
    {
        AbilityValues abilityInfo;

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
