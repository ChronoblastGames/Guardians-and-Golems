﻿using System.Collections;
using UnityEngine;

public class MightOfTheEarth : AbilityCastBase
{
    [Header("Ability Attributes")]
    public LayerMask conduitMask;

    public override void InitializeAbility()
    {
        CheckArea();

        GiveShield();

        castAudio.Play();

        Destroy(gameObject, abilityValues.activeTime);
    }

    void GiveShield()
    {
        abilityValues.casterGameObject.GetComponent<GolemResources>().InflictStatusEffect(abilityValues.statusEffect, abilityValues.effectStrength, abilityValues.effectTime, abilityValues.effectFrequency, gameObject, abilityValues.casterGameObject);
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
                    hitColliders[i].GetComponent<ConduitController>().DisableConduit(abilityValues.effectTime, PlayerTeam.RED);
                }
            }
            else if (gameObject.layer == LayerMask.NameToLayer("GolemBlue"))
            {
                if (hitColliders[i].CompareTag("Conduit"))
                {
                    hitColliders[i].GetComponent<ConduitController>().DisableConduit(abilityValues.effectTime, PlayerTeam.BLUE);
                }
            }
        }
    }
}
