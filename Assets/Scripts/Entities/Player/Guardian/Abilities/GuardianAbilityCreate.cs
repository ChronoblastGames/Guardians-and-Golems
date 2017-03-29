using System.Collections;
using UnityEngine;

public class GuardianAbilityCreate : CreateAbilityBase
{
    private GuardianInputManager guardianInputManager;
    private GuardianPlayerController guardianPlayerController;
    private GuardianResources guardianResources;
    private GuardianCooldownManager guardianCooldown;

    [Header("Ability Type")]
    public AbilityType abilityType;

    [Header("Ability Mask")]
    public LayerMask redMask;
    public LayerMask blueMask;

    public float minHoldTime = 0.25f;

    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        guardianInputManager = transform.parent.parent.GetComponent<GuardianInputManager>();
        guardianPlayerController = guardianInputManager.GetComponent<GuardianPlayerController>();    
        guardianResources = guardianInputManager.GetComponent<GuardianResources>();
        guardianCooldown = guardianInputManager.GetComponent<GuardianCooldownManager>();
    }

    public override void CastGuardianAbility(PlayerTeam teamColor, float holdTime, GameObject spawnObject, GameObject casterObject)
    {
        AbilityValues newAbilityValues = CreateAbilityStruct();

        newAbilityValues.casterGameObject = casterObject;

        if (holdTime < minHoldTime)
        {
            holdTime = 0;
        }

        newAbilityValues.holdTime = holdTime;

        StartCoroutine(FireAbility(teamColor, newAbilityValues, spawnObject));
    }

    private IEnumerator FireAbility(PlayerTeam teamColor, AbilityValues abilityInfo, GameObject spawnObject)
    {
        guardianPlayerController.isUsingAbility = true;

        Vector3 storedAimVec = guardianInputManager.aimVec;

        if (abilityInfo.abilityCastTime > 0)
        {
            guardianPlayerController.canMove = false;

            yield return new WaitForSeconds(abilityInfo.abilityCastTime);

            guardianPlayerController.canMove = true;
        }

        Vector3 newSpawnPosition = Vector3.zero;

        Vector3 newAimVector = guardianInputManager.aimVec;

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

        newAbility.GetComponent<AbilityCastBase>().abilityValues = abilityInfo;
        newAbility.GetComponent<AbilityCastBase>().InitializeAbility();

        guardianPlayerController.isUsingAbility = false;

        guardianCooldown.QueueGlobalCooldown();
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
