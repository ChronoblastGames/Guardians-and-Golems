using System.Collections;
using UnityEngine;

public class AbilityCreate : AbilityBase 
{
    private GolemPlayerController golemPlayerController;
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

    public override void CastAbility(Vector3 aimVec, PlayerTeam teamColor, float heldTime, GameObject casterObject)
    {
        AbilityValues abilityValues = CreateAbilityStruct();
        Vector3 spawnVec = Vector3.zero;
        Quaternion spawnRot = Quaternion.identity;

        golemPlayerController = casterObject.GetComponent<GolemPlayerController>();

        abilityValues.casterGameObject = casterObject;

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

                if (spawnPos != null)
                {
                    spawnVec = spawnPos.transform.position;
                }
                else
                {
                    spawnVec = transform.position + new Vector3(0, 1, 0) + new Vector3(0, 0, abilityValues.spawnDistanceFromPlayer);
                }

                StartCoroutine(FireAbility(ability, spawnVec, aimVec, abilityValues.abilityCastTime, teamColor, null, abilityValues));

                break;

            case AbilityType.STATIC:

                spawnVec = aimVec;
                spawnVec.Normalize();
                spawnVec = spawnVec * abilityValues.spawnDistanceFromPlayer;

                spawnVec = transform.position + spawnVec;

                StartCoroutine(FireAbility(ability, spawnVec, aimVec, abilityValues.abilityCastTime, teamColor, null, abilityValues));

                break;

            case AbilityType.ZONE:

                spawnVec = aimVec * abilityValues.spawnDistanceFromPlayer;
                spawnVec.y = 0f;

                spawnVec = transform.position + spawnVec;

                StartCoroutine(FireAbility(ability, spawnVec, aimVec, abilityValues.abilityCastTime, teamColor, null, abilityValues));

                break;
        }       
    }

    private IEnumerator FireAbility(GameObject ability, Vector3 spawnPos, Vector3 aimVector, float castTime, PlayerTeam teamColor, GameObject targetSpawnPos, AbilityValues abilityInfo)
    {
        if (castTime > 0)
        {
            golemPlayerController.StopMovement();

            yield return new WaitForSeconds(castTime);

            golemPlayerController.StartMovement();
        }

        Quaternion spawnRot = Quaternion.LookRotation(aimVector);

        GameObject newAbility = Instantiate(ability, spawnPos, spawnRot) as GameObject;

        newAbility.GetComponent<GolemAbilityBase>().abilityValues = abilityInfo;
        newAbility.GetComponent<GolemAbilityBase>().InitializeAbility();

        if (teamColor == PlayerTeam.RED)
        {
            newAbility.layer = LayerMask.NameToLayer("GolemRed");
        }
        else if (teamColor == PlayerTeam.BLUE)
        {
            newAbility.layer = LayerMask.NameToLayer("GolemBlue");
        }
    }

    public AbilityValues CreateAbilityStruct()
    {
        AbilityValues abilityInfo;

        abilityInfo.casterGameObject = casterGameObject;
        abilityInfo.createPoint = spawnPos;
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
