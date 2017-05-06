using System.Collections;
using UnityEngine;

public class GolemAbilityCreate : CreateAbilityBase 
{
    private CrystalManager crystalManager;

    private GolemInputController golemInputController;
    private GolemPlayerController golemPlayerController;
    private GolemResources golemResources;
    private GolemCooldownManager golemCooldown;

    [Header("Ability Type")]
    public AbilitySubType abilitySubType;

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
        crystalManager = GameObject.FindGameObjectWithTag("CrystalManager").GetComponent<CrystalManager>();

        golemInputController = transform.parent.parent.GetComponent<GolemInputController>();
        golemPlayerController = golemInputController.GetComponent<GolemPlayerController>();
        golemResources = golemInputController.GetComponent<GolemResources>();
        golemCooldown = golemInputController.GetComponent<GolemCooldownManager>();
    }

    public override void CastAbility(PlayerTeam newTeamColor, float heldTime, GameObject casterObject)
    {
        AbilityValues newAblityValues = CreateAbilityStruct();

        newAblityValues.teamColor = newTeamColor;

        newAblityValues.casterGameObject = casterObject;

        if (heldTime < minHoldTime)
        {
            heldTime = 0;
        }

        newAblityValues.holdTime = heldTime;

        StartCoroutine(FireAbility(newTeamColor, newAblityValues));     
    }

    private IEnumerator FireAbility(PlayerTeam teamColor, AbilityValues abilityInfo)
    {
        golemPlayerController.isCastingAbility = true;

        Vector3 storedAimVec = golemInputController.aimVec;

        if (abilityInfo.abilityCastTime > 0)
        {
            golemPlayerController.StopMovement();

            yield return new WaitForSeconds(abilityInfo.abilityCastTime);

            golemPlayerController.StartMovement();
        }

        Vector3 newSpawnPosition = Vector3.zero;

        Vector3 newAimVector = golemInputController.aimVec;

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
                    newSpawnPosition = transform.position + Vector3.up + new Vector3(0, 0, abilityInfo.spawnDistanceFromPlayer);
                }

                break;

            case AbilitySubType.STATIC:

                newSpawnPosition = newAimVector;
                newSpawnPosition = newSpawnPosition * abilityInfo.spawnDistanceFromPlayer;

                newSpawnPosition = transform.position + newSpawnPosition;

                break;

            case AbilitySubType.ZONE:

                newSpawnPosition = newAimVector * abilityInfo.spawnDistanceFromPlayer;
                newSpawnPosition.y = 0f;

                newSpawnPosition = transform.position + newSpawnPosition;

                break;
        }

        newSpawnRotation = Quaternion.LookRotation(newAimVector);

        if (crystalManager.UseCrystals(abilityInfo.crystalCost, abilityInfo.teamColor, PlayerType.GOLEM))
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

            golemPlayerController.isCastingAbility = false;

            golemCooldown.QueueGlobalCooldown();
        }
    }

    public AbilityValues CreateAbilityStruct()
    {
        AbilityValues abilityInfo;

        abilityInfo.abilityType = abilityType;
        abilityInfo.indicatorType = indicatorType;
        abilityInfo.indicatorSize = indicatorSize;
        abilityInfo.casterGameObject = casterGameObject;
        abilityInfo.casterConduit = casterConduit;
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
