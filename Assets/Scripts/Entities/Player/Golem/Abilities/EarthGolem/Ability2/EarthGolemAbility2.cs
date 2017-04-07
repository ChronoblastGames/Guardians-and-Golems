using System.Collections;
using UnityEngine;

public class EarthGolemAbility2 : AbilityCastBase 
{
    private TimerClass activeTimer;

    private Animator boulderAnimator;

    private bool isTimerActive;
	
	void FixedUpdate () 
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

        boulderAnimator = GetComponent<Animator>();

        if (abilityValues.activeTime > 0)
        {
            activeTimer.ResetTimer(abilityValues.activeTime);

            isTimerActive = true;
        }

        isAbilityActive = true;
    }

    void AbilityControl()
    {
        transform.position += transform.forward * abilityValues.travelSpeed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isAbilityActive)
        {
            if (gameObject.layer == LayerMask.NameToLayer("GolemRed"))
            {
                if (other.gameObject.CompareTag("GolemBlue"))
                {
                    boulderAnimator.SetTrigger("isShatter");

                    other.gameObject.GetComponent<GolemResources>().TakeDamage(abilityValues.damageAmount, abilityValues.damageType, abilityValues.statusEffect, abilityValues.effectStrength, abilityValues.effectTime, abilityValues.effectFrequency, gameObject);

                    Destroy(gameObject, 2f);
                    isAbilityActive = false;
                }
            }
            else if (gameObject.layer == LayerMask.NameToLayer("GolemBlue"))
            {
                if (other.gameObject.CompareTag("GolemRed"))
                {
                    boulderAnimator.SetTrigger("isShatter");

                    other.gameObject.GetComponent<GolemResources>().TakeDamage(abilityValues.damageAmount, abilityValues.damageType, abilityValues.statusEffect, abilityValues.effectStrength, abilityValues.effectTime, abilityValues.effectFrequency, gameObject);

                    Destroy(gameObject, 2f);
                    isAbilityActive = false;
                }
            }
        }   
    }

    private void OnCollisionEnter(Collision collision)
    {
        boulderAnimator.SetTrigger("isShatter");

        isAbilityActive = false;
        Destroy(gameObject, 2f);
    }
}
