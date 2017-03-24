using System.Collections;
using UnityEngine;

public class GolemAbilityCreate : AbilityBase 
{
    private GolemInputManager golemInputManager;
    private GolemPlayerController golemPlayerController;
    private GolemResources golemResources;
    private CooldownManager golemCooldown;

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
        golemInputManager = transform.parent.parent.GetComponent<GolemInputManager>();
        golemPlayerController = golemInputManager.GetComponent<GolemPlayerController>();
        golemResources = golemInputManager.GetComponent<GolemResources>();
        golemCooldown = golemInputManager.GetComponent<CooldownManager>();
    }

    public override void CastAbility(PlayerTeam teamColor, float heldTime, GameObject casterObject)
    {
        AbilityValues newAblityValues = CreateAbilityStruct();

        newAblityValues.casterGameObject = casterObject;

        if (heldTime < minHoldTime)
        {
            heldTime = 0;
        }

        newAblityValues.holdTime = heldTime;

        StartCoroutine(FireAbility(teamColor, newAblityValues));     
    }

    private IEnumerator FireAbility(PlayerTeam teamColor, AbilityValues abilityInfo)
    {
        golemPlayerController.isCastingAbility = true;

        Vector3 storedAimVec = golemInputManager.aimVec;

        if (abilityInfo.abilityCastTime > 0)
        {
            golemPlayerController.StopMovement();

            yield return new WaitForSeconds(abilityInfo.abilityCastTime);

            golemPlayerController.StartMovement();
        }

        Vector3 newSpawnPosition = Vector3.zero;

        Vector3 newAimVector = golemInputManager.aimVec;

        if (newAimVector != storedAimVec)
        {
            if (newAimVector == Vector3.zero)
            {
                newAimVector = storedAimVec;
            }
        }
        else if (newAimVector == Vector3.zero)
        {
            newAimVector = transform.forward;
        }

        Quaternion newSpawnRotation = Quaternion.identity;

        switch (abilityType)
        {
            case AbilityType.PROJECTILE:

                if (abilityInfo.createPoint != null)
                {
                    newSpawnPosition = abilityInfo.createPoint.transform.position;
                }
                else
                {
                    newSpawnPosition = transform.position + new Vector3(0, 1, 0) + new Vector3(0, 0, abilityInfo.spawnDistanceFromPlayer);
                }

                break;

            case AbilityType.STATIC:

                newSpawnPosition = newAimVector;
                newSpawnPosition = newSpawnPosition * abilityInfo.spawnDistanceFromPlayer;

                newSpawnPosition = transform.position + newSpawnPosition;

                break;

            case AbilityType.ZONE:

                newSpawnPosition = newAimVector * abilityInfo.spawnDistanceFromPlayer;
                newSpawnPosition.y = 0f;

                newSpawnPosition = transform.position + newSpawnPosition;

                break;
        }

        newSpawnRotation = Quaternion.LookRotation(newAimVector);

        GameObject newAbility = Instantiate(ability, newSpawnPosition, newSpawnRotation) as GameObject;
  
        if (teamColor == PlayerTeam.RED)
        {
            newAbility.layer = LayerMask.NameToLayer("GolemRed");
        }
        else if (teamColor == PlayerTeam.BLUE)
        {
            newAbility.layer = LayerMask.NameToLayer("GolemBlue");
        }

        newAbility.GetComponent<GolemAbilityBase>().abilityValues = abilityInfo;
        newAbility.GetComponent<GolemAbilityBase>().InitializeAbility();

        golemPlayerController.isCastingAbility = false;

        golemCooldown.QueueGlobalCooldown();
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
