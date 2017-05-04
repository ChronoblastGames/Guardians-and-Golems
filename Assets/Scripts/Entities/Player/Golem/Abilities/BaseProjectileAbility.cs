using System.Collections;
using UnityEngine;

public class BaseProjectileAbility : AbilityCastBase
{
    private TimerClass projectileTimer;

    private Rigidbody myRB;
    private GameObject trailRenderer;

    public bool hasTime;

    private void Update()
    {
        CheckTimer();
    }

    void CheckTimer()
    {
        if (hasTime)
        {
            if (projectileTimer.TimerIsDone())
            {
                HideSelf();
            }
        }
    }

    public override void InitializeAbility()
    {
        myRB = GetComponent<Rigidbody>();

        trailRenderer = transform.GetChild(0).gameObject;

        if (abilityValues.activeTime > 0)
        {
            projectileTimer = new TimerClass();

            projectileTimer.ResetTimer(abilityValues.activeTime);

            hasTime = true;
        }

        UseAbility();
    }

    void UseAbility()
    {
        myRB.AddForce(transform.forward * abilityValues.travelSpeed, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GolemRed") || other.gameObject.CompareTag("GolemBlue"))
        {
            other.gameObject.GetComponent<GolemResources>().TakeDamage(abilityValues.damageAmount, abilityValues.damageType, abilityValues.statusEffect, abilityValues.effectStrength, abilityValues.effectTime, abilityValues.effectFrequency, gameObject, abilityValues.casterGameObject);
            HideSelf();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        HideSelf();
    }

    public void HideSelf ()
    {
       trailRenderer.transform.parent = null;
       gameObject.SetActive(false);
       Invoke("DestroyTrail", 1);
    }

    void DestroyTrail ()
    {
        Destroy(trailRenderer.gameObject);
        Destroy(gameObject);
    }
}
