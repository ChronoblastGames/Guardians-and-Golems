using System.Collections;
using UnityEngine;

public class GuardianAbilityCreate : CreateAbilityBase
{
    private CrystalManager crystalManager;

    private GuardianInputController guardianInputController;
    private GuardianPlayerController guardianPlayerController;
    private GuardianResources guardianResources;
    private GuardianCooldownManager guardianCooldown;

    [Header("Ability Type")]
    public AbilitySubType abilitySubType;

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
        crystalManager = GameObject.FindGameObjectWithTag("CrystalManager").GetComponent<CrystalManager>();

        guardianInputController = transform.parent.parent.GetComponent<GuardianInputController>();
        guardianPlayerController = guardianInputController.GetComponent<GuardianPlayerController>();    
        guardianResources = guardianInputController.GetComponent<GuardianResources>();
        guardianCooldown = guardianInputController.GetComponent<GuardianCooldownManager>();
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

        StartCoroutine(FireAbility(teamColor, newAbilityValues, casterObject));
    }

    private IEnumerator FireAbility(PlayerTeam teamColor, AbilityValues abilityInfo, GameObject spawnObject)
    {
        guardianPlayerController.isUsingAbility = true;

        Vector3 storedAimVec = guardianInputController.aimVec;

        if (abilityInfo.abilityCastTime > 0)
        {
            guardianPlayerController.canMove = false;

            yield return new WaitForSeconds(abilityInfo.abilityCastTime);

            guardianPlayerController.canMove = true;
        }

        Vector3 newSpawnPosition = Vector3.zero;

        Vector3 newAimVector = guardianInputController.aimVec;

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

        switch (abilitySubType)
        {
            case AbilitySubType.PROJECTILE:

                if (abilityInfo.createPoint != null)
                {
                    newSpawnPosition = abilityInfo.createPoint.transform.position;
                }
                else
                {
                    newSpawnPosition = spawnObject.transform.position + Vector3.up + new Vector3(0, 0, abilityInfo.spawnDistanceFromPlayer);
                }

                break;

            case AbilitySubType.STATIC:

                newSpawnPosition = newAimVector;
                newSpawnPosition = newSpawnPosition * abilityInfo.spawnDistanceFromPlayer;

                newSpawnPosition = spawnObject.transform.position + newSpawnPosition;

                break;

            case AbilitySubType.ZONE:

                newSpawnPosition = newAimVector * abilityInfo.spawnDistanceFromPlayer;
                newSpawnPosition.y = 0f;

                newSpawnPosition = spawnObject.transform.position + newSpawnPosition;

                break;
        }

        newSpawnRotation = Quaternion.LookRotation(newAimVector);

        if (crystalManager.UseCrystals(abilityInfo.crystalCost, abilityInfo.teamColor, PlayerType.GUARDIAN))
        {
            GameObject newAbility = Instantiate(ability, newSpawnPosition, newSpawnRotation) as GameObject;

            if (abilitySubType == AbilitySubType.ZONE)
            {
                newAbility.layer = LayerMask.NameToLayer("Abilities");
            }
            else
            {
                if (teamColor == PlayerTeam.RED)
                {
                    newAbility.layer = LayerMask.NameToLayer("GolemRed");
                }
                else if (teamColor == PlayerTeam.BLUE)
                {
                    newAbility.layer = LayerMask.NameToLayer("GolemBlue");
                }
            }

            newAbility.GetComponent<AbilityCastBase>().abilityValues = abilityInfo;
            newAbility.GetComponent<AbilityCastBase>().InitializeAbility();

            guardianPlayerController.isUsingAbility = false;

            guardianCooldown.QueueGlobalCooldown();
        }     
    }

    public AbilityValues CreateAbilityStruct()
    {
        AbilityValues abilityInfo;

        abilityInfo.abilityType = abilityType;
        abilityInfo.indicatorType = indicatorType;
        abilityInfo.indicatorSize = indicatorSize;
        abilityInfo.casterGameObject = casterGameObject;
        abilityInfo.teamColor = teamColor;
        abilityInfo.createPoint = spawnPos;
        abilityInfo.abilityCastTime = abilityCastTime;
        abilityInfo.damageType = damageType;
        abilityInfo.damageAmount = damageAmount;
        abilityInfo.damageFrequency = damageFrequency;
        abilityInfo.activeTime = activeTime;
        abilityInfo.holdTime = holdTime;
        abilityInfo.travelSpeed = travelSpeed;
        abilityInfo.spawnDistanceFromPlayer = spawnDistanceFromPlayer;
        abilityInfo.zoneRadius = zoneRadius;
        abilityInfo.zoneHeight = zoneHeight;
        abilityInfo.isMelee = isMelee;
        abilityInfo.isRanged = isRanged;
        abilityInfo.isHeld = isHeld;
        abilityInfo.healthCost = healthCost;
        abilityInfo.crystalCost = crystalCost;
        abilityInfo.statusEffect = statusEffect;
        abilityInfo.effectStrength = effectStrength;
        abilityInfo.effectTime = effectTime;
        abilityInfo.effectFrequency = effectFrequency;

        return abilityInfo;
    }
}
