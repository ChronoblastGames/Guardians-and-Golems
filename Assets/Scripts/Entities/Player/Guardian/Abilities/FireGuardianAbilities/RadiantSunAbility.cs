using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiantSunAbility : AbilityCastBase
{
    [Header("Ability Attributes")]
    public float healStrength;
    public float healFrequency;

    private List<GameObject> recentlyHealedList;

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
            if (gameObject.layer == LayerMask.NameToLayer("GolemRed"))
            {
                if (other.gameObject.CompareTag("GolemRed"))
                {
                    if (!recentlyHealedList.Contains(other.gameObject))
                    {
                        other.gameObject.GetComponent<GolemResources>().GetHealed(abilityValues.damageAmount, abilityValues.statusEffect, abilityValues.effectStrength, abilityValues.effectTime, abilityValues.casterGameObject);
                        ManageHealing(other.gameObject, healFrequency);
                    }
                }
            }
            else if (gameObject.layer == LayerMask.NameToLayer("GolemBlue"))
            {
                if (other.gameObject.CompareTag("GolemBlue"))
                {
                    if (!recentlyHealedList.Contains(other.gameObject))
                    {
                        other.gameObject.GetComponent<GolemResources>().GetHealed(abilityValues.damageAmount, abilityValues.statusEffect, abilityValues.effectStrength, abilityValues.effectTime, abilityValues.casterGameObject);
                        ManageHealing(other.gameObject, healFrequency);
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
