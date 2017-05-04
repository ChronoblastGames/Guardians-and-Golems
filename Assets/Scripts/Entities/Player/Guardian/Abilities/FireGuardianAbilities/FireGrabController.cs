using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGrabController : AbilityCastBase
{
    private Projector abilityProjector;

    private void FixedUpdate()
    {
        if (isAbilityActive)
        {
            MoveHand();
        }
    }

    public override void InitializeAbility()
    {
        abilityProjector = transform.GetChild(0).GetComponent<Projector>();

        if (abilityValues.teamColor == PlayerTeam.RED)
        {
            abilityProjector.material.color = Colors.YellowTeamColor;
        }
        else if (abilityValues.teamColor == PlayerTeam.BLUE)
        {
            abilityProjector.material.color = Colors.BlueTeamColor;
        }

        if (abilityValues.activeTime > 0)
        {
            Destroy(gameObject, abilityValues.activeTime);
        }

        isAbilityActive = true;

        castAudio.Play();
    }

    private void MoveHand()
    {
        transform.position = transform.position + transform.forward * -abilityValues.travelSpeed * Time.deltaTime;  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isAbilityActive)
        {
            if (abilityValues.teamColor == PlayerTeam.RED)
            {
                if (other.gameObject.CompareTag("GolemBlue"))
                {
                    other.gameObject.GetComponent<GolemResources>().TakeDamage(abilityValues.damageAmount, abilityValues.damageType, abilityValues.statusEffect, abilityValues.effectStrength, abilityValues.effectTime, abilityValues.effectFrequency, gameObject, abilityValues.casterGameObject);
                }
            }
            else if (abilityValues.teamColor == PlayerTeam.BLUE)
            {
                if (other.gameObject.CompareTag("GolemRed"))
                {
                    other.gameObject.GetComponent<GolemResources>().TakeDamage(abilityValues.damageAmount, abilityValues.damageType, abilityValues.statusEffect, abilityValues.effectStrength, abilityValues.effectTime, abilityValues.effectFrequency, gameObject, abilityValues.casterGameObject);
                }
            }
        }     
    }
}
