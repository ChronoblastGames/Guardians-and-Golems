using System.Collections;
using UnityEngine;

public class EarthGolemAbility3 : AbilityCastBase
{
    [Header("Ability Attributes")]
    public LayerMask conduitMask;

    public override void InitializeAbility()
    {
        CheckArea();

        GiveShield();

        Destroy(gameObject, abilityValues.activeTime);
    }

    void GiveShield()
    {
        abilityValues.casterGameObject.GetComponent<GolemResources>().InflictStatusEffect(abilityValues.statusEffect, abilityValues.effectStrength, abilityValues.effectTime, gameObject);
    }

    void CheckArea()
    {
        isAbilityActive = true;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, abilityValues.zoneRadius, conduitMask);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (gameObject.layer == LayerMask.NameToLayer("GolemRed"))
            {
                if (hitColliders[i].CompareTag("Conduit"))
                {
                    hitColliders[i].GetComponent<OrbController>().DisableOrb(abilityValues.effectTime, PlayerTeam.RED);
                }
            }
            else if (gameObject.layer == LayerMask.NameToLayer("GolemBlue"))
            {
                if (hitColliders[i].CompareTag("Conduit"))
                {
                    hitColliders[i].GetComponent<OrbController>().DisableOrb(abilityValues.effectTime, PlayerTeam.BLUE);
                }
            }
        }
    }
}
