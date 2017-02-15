using System.Collections;
using UnityEngine;

public class GuardianAbility : BaseGuardianAbilities
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

    public override void CastAbility(Vector3 aimVec, Vector3 spawnPos, PlayerTeam teamColor)
    {
        AbilityValues abilityValues;
        Vector3 spawnVec;
        Quaternion spawnRot;

        switch (abilityType)
        {
            case AbilityType.BUFF:
                break;

            case AbilityType.DEBUFF:
                break;

            case AbilityType.PROJECTILE:

                abilityValues = CreateAbilityStruct();

                if (guardianResources.CanCast(abilityValues.manaCost))
                {
                    spawnRot = Quaternion.LookRotation(aimVec);

                    spawnVec = transform.position + new Vector3(0, 1, 0) + new Vector3(0, 0, abilityValues.spawnDistanceFromPlayer);

                    GameObject newProjectile = Instantiate(ability, spawnVec, spawnRot) as GameObject;
                    newProjectile.GetComponent<BaseProjectileAbility>().abilityValues = abilityValues;

                    if (teamColor == PlayerTeam.RED)
                    {
                        newProjectile.layer = 8;
                    }
                    else if (teamColor == PlayerTeam.BLUE)
                    {
                        newProjectile.layer = 9;
                    }
                }
                break;

            case AbilityType.STATIC:

                abilityValues = CreateAbilityStruct();

                if (guardianResources.CanCast(abilityValues.manaCost))
                {
                    spawnRot = Quaternion.LookRotation(aimVec);

                    spawnVec = aimVec;
                    spawnVec.Normalize();
                    spawnVec = spawnVec * abilityValues.spawnDistanceFromPlayer;
                    spawnVec.y = -5f;

                    GameObject newStaticAbility = Instantiate(ability, transform.position + spawnVec, spawnRot) as GameObject;
                    newStaticAbility.GetComponent<BaseStaticAbility>().abilityValues = abilityValues;
                    newStaticAbility.GetComponent<BaseStaticAbility>().InitializeWall();
                }
                break;

            case AbilityType.ZONE:

                abilityValues = CreateAbilityStruct();

                if (guardianResources.CanCast(abilityValues.manaCost))
                {
                    if (aimVec != Vector3.zero)
                    {
                        spawnRot = Quaternion.LookRotation(aimVec);

                        Vector3 zoneSpawnVec = aimVec;
                        zoneSpawnVec.Normalize();
                        zoneSpawnVec = zoneSpawnVec * abilityValues.spawnDistanceFromPlayer;
                        zoneSpawnVec.y = 0f;

                        GameObject newZoneAbility = Instantiate(ability, transform.position + zoneSpawnVec, spawnRot) as GameObject;
                        newZoneAbility.GetComponent<BaseZoneAbility>().abilityValues = abilityValues;
                        newZoneAbility.GetComponent<BaseZoneAbility>().InitializeAbility();
                    }
                    else
                    {
                        Debug.Log("Should cast on myself");
                        GameObject newZoneAbility = Instantiate(ability, transform.position, Quaternion.identity) as GameObject;
                        newZoneAbility.GetComponent<BaseZoneAbility>().abilityValues = abilityValues;
                        newZoneAbility.GetComponent<BaseZoneAbility>().InitializeAbility();
                    }
                }
                break;
        }
    }

    public AbilityValues CreateAbilityStruct()
    {
        AbilityValues abilityInfo;

        abilityInfo.damageType = damageType;
        abilityInfo.damageAmount = damageAmount;
        abilityInfo.projectileSpeed = projectileSpeed;
        abilityInfo.spawnDistanceFromPlayer = spawnDistanceFromPlayer;
        abilityInfo.activeTime = activeTime;
        abilityInfo.raiseAmount = raiseAmount;
        abilityInfo.raiseSpeed = raiseSpeed;
        abilityInfo.zoneRadius = zoneRadius;
        abilityInfo.zoneHeight = zoneHeight;
        abilityInfo.zoneStrength = zoneStrength;
        abilityInfo.zoneTime = zoneTime;
        abilityInfo.isMelee = isMelee;
        abilityInfo.isRanged = isRanged;
        abilityInfo.healthCost = healthCost;
        abilityInfo.manaCost = manaCost;
        abilityInfo.canStun = canStun;
        abilityInfo.canSlow = canSlow;
        abilityInfo.canDrainHealth = canDrainHealth;
        abilityInfo.canDrainMana = canDrainMana;
        abilityInfo.canBlind = canBlind;

        return abilityInfo;
    }
}
