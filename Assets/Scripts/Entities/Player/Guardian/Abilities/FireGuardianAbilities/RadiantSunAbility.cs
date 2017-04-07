using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiantSunAbility : AbilityCastBase
{
    public List<GameObject> recentlyHealedList;

    public override void InitializeAbility()
    {
        if (abilityValues.activeTime > 0)
        {
            Destroy(gameObject, abilityValues.activeTime);
        }

        isAbilityActive = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (isAbilityActive)
        {
            if (abilityValues.teamColor == PlayerTeam.RED)
            {
                if (other.gameObject.CompareTag("GolemRed"))
                {
                    if (!recentlyHealedList.Contains(other.gameObject))
                    {
                        other.gameObject.GetComponent<GolemResources>().GetHealed(abilityValues.damageAmount, abilityValues.statusEffect, abilityValues.effectStrength, abilityValues.effectTime, abilityValues.effectFrequency, abilityValues.casterGameObject);
                        StartCoroutine(ManageHealing(other.gameObject, abilityValues.damageFrequency));
                    }
                }
            }
            else if (abilityValues.teamColor == PlayerTeam.BLUE)
            {
                if (other.gameObject.CompareTag("GolemBlue"))
                {
                    if (!recentlyHealedList.Contains(other.gameObject))
                    {
                        other.gameObject.GetComponent<GolemResources>().GetHealed(abilityValues.damageAmount, abilityValues.statusEffect, abilityValues.effectStrength, abilityValues.effectTime, abilityValues.effectFrequency, abilityValues.casterGameObject);
                        StartCoroutine(ManageHealing(other.gameObject, abilityValues.damageFrequency));
                    }
                }
            }
        }
    }  
    
    private IEnumerator ManageHealing(GameObject healedObject, float healFrequency)
    {
        recentlyHealedList.Add(healedObject);
    
        if (healFrequency > 0)
        {
            yield return new WaitForSeconds(healFrequency);
        }

        recentlyHealedList.Remove(healedObject);

        yield return null;
    }  
}
