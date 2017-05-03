using System.Collections;
using UnityEngine;

public class BoulderToss : AbilityCastBase 
{
    public Material yellowMaterial;
    public Material blueMaterial;

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

        if (abilityValues.teamColor == PlayerTeam.RED)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Renderer>().material = yellowMaterial;
            }
        }
        else if (abilityValues.teamColor == PlayerTeam.BLUE)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Renderer>().material = blueMaterial;
            }
        }

        castAudio.Play();

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

                    other.gameObject.GetComponent<GolemResources>().TakeDamage(abilityValues.damageAmount, abilityValues.damageType, abilityValues.statusEffect, abilityValues.effectStrength, abilityValues.effectTime, abilityValues.effectFrequency, gameObject, abilityValues.casterGameObject);

                    Destroy(gameObject, 2f);
                    isAbilityActive = false;

                    collisionAudio.Play();
                }
            }
            else if (gameObject.layer == LayerMask.NameToLayer("GolemBlue"))
            {
                if (other.gameObject.CompareTag("GolemRed"))
                {
                    boulderAnimator.SetTrigger("isShatter");

                    other.gameObject.GetComponent<GolemResources>().TakeDamage(abilityValues.damageAmount, abilityValues.damageType, abilityValues.statusEffect, abilityValues.effectStrength, abilityValues.effectTime, abilityValues.effectFrequency, gameObject, abilityValues.casterGameObject);

                    Destroy(gameObject, 2f);
                    isAbilityActive = false;

                    collisionAudio.Play();
                }
            }
        }   
    }

    private void OnCollisionEnter(Collision collision)
    {
        boulderAnimator.SetTrigger("isShatter");

        isAbilityActive = false;
        Destroy(gameObject, 2f);

        collisionAudio.Play();
    }
}
