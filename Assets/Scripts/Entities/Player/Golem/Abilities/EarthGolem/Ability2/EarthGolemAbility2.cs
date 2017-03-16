using System.Collections;
using UnityEngine;

public class EarthGolemAbility2 : GolemAbilityBase 
{
    private TimerClass activeTimer;

    private bool isTimerActive;
	
	void Update () 
    {
        if (isAbilityActive)
        {
            AbilityControl();
        }

		if (isTimerActive)
        {
            if (activeTimer.TimerIsDone())
            {
                Destroy(gameObject);
            }
        }
	}

    public override void InitializeAbility()
    {
        activeTimer = new TimerClass();

        if (abilityValues.activeTime > 0)
        {
            activeTimer.ResetTimer(abilityValues.activeTime);

            isTimerActive = true;
        }

        isAbilityActive = true;
    }

    void AbilityControl()
    {
        transform.position += transform.forward * abilityValues.projectileSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.layer == LayerMask.NameToLayer("GolemRed"))
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("GolemBlue"))
            {
                other.gameObject.GetComponent<GolemResources>().TakeDamage(abilityValues.damageAmount, abilityValues.damageType, abilityValues.statusEffect, abilityValues.effectStrength, abilityValues.effectTime, gameObject);
                Destroy(gameObject);
            }
        }
        else if (gameObject.layer == LayerMask.NameToLayer("GolemBlue"))
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("GolemRed"))
            {
                other.gameObject.GetComponent<GolemResources>().TakeDamage(abilityValues.damageAmount, abilityValues.damageType, abilityValues.statusEffect, abilityValues.effectStrength, abilityValues.effectTime, gameObject);
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
