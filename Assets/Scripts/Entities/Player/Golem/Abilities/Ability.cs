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

                AbilityValues newAbilityValues = CreateAbilityStruct();

                Quaternion newProjectileRotation = Quaternion.LookRotation(aimVec);

                GameObject newProjectile = Instantiate(ability, transform.position + new Vector3(0, 1, 0) + new Vector3(0, 0, newAbilityValues.spawnDistanceFromPlayer), newProjectileRotation) as GameObject;
                newProjectile.GetComponent<BaseProjectileAbility>().abilityValues = newAbilityValues;
                
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
                break;

            case AbilityType.ZONE:
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
        abilityInfo.rangeDistance = rangeDistance;
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
