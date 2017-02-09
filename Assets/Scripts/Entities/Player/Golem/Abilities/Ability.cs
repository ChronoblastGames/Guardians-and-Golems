using System.Collections;
using UnityEngine;

public class Ability : GolemAbility 
{
    [Header("Ability Type")]
    public AbilityType abilityType;

    [Header("Ability Mask")]
    public LayerMask redMask;
    public LayerMask blueMask;

    public override void CastAbility(Vector3 aimVec, string teamColor)
    {
        switch(abilityType)
        {
            case AbilityType.BUFF:
                break;

            case AbilityType.DEBUFF:
                break;

            case AbilityType.PROJECTILE:

                AbilityValues newProjectileAbilityValues = CreateAbilityStruct();

                Quaternion newProjectileRotation = Quaternion.LookRotation(aimVec);

                GameObject newProjectile = Instantiate(ability, transform.position + new Vector3(0, 1, 0) + new Vector3(0, 0, newProjectileAbilityValues.spawnDistanceFromPlayer), newProjectileRotation) as GameObject;
                newProjectile.GetComponent<BaseProjectileAbility>().abilityValues = newProjectileAbilityValues;
                
                if (teamColor == "Red")
                {
                    newProjectile.layer = 8;
                }
                else if (teamColor == "Blue")
                {
                    newProjectile.layer = 9;
                }

                break;

            case AbilityType.STATIC:

                AbilityValues newStaticAbilityValues = CreateAbilityStruct();

                Quaternion newStaticRotation = Quaternion.LookRotation(aimVec);

                Vector3 staticSpawnVec = aimVec;
                staticSpawnVec.Normalize();
                staticSpawnVec = staticSpawnVec * newStaticAbilityValues.spawnDistanceFromPlayer;
                staticSpawnVec.y = -5f;

                GameObject newStaticAbility = Instantiate(ability, transform.position + staticSpawnVec, newStaticRotation) as GameObject;
                newStaticAbility.GetComponent<BaseStaticAbility>().abilityValues = newStaticAbilityValues;
                newStaticAbility.GetComponent<BaseStaticAbility>().InitializeWall();
                break;

            case AbilityType.ZONE:

                AbilityValues newZoneAbilityValues = CreateAbilityStruct();

                if (aimVec != Vector3.zero)
                {
                    Quaternion newZoneRotation = Quaternion.LookRotation(aimVec);

                    Vector3 zoneSpawnVec = aimVec;
                    zoneSpawnVec.Normalize();
                    zoneSpawnVec = zoneSpawnVec * newZoneAbilityValues.spawnDistanceFromPlayer;
                    zoneSpawnVec.y = 0f;

                    GameObject newZoneAbility = Instantiate(ability, transform.position + zoneSpawnVec, newZoneRotation) as GameObject;
                    newZoneAbility.GetComponent<BaseZoneAbility>().abilityValues = newZoneAbilityValues;
                    newZoneAbility.GetComponent<BaseZoneAbility>().InitializeAbility();
                }
                else
                {
                    Debug.Log("Should cast on myself");
                    GameObject newZoneAbility = Instantiate(ability, transform.position, Quaternion.identity) as GameObject;
                    newZoneAbility.GetComponent<BaseZoneAbility>().abilityValues = newZoneAbilityValues;
                    newZoneAbility.GetComponent<BaseZoneAbility>().InitializeAbility();
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
