using System.Collections;
using UnityEngine;

public class BaseZoneAbility : GolemAbilityBase
{
    private TimerClass zoneTimer;

    private bool isInitialized = false;

    void Update()
    {     
        ManageZoneTime();
    }

    public override void InitializeAbility()
    {
        zoneTimer = new TimerClass();
      
        transform.localScale = new Vector3(abilityValues.zoneRadius, abilityValues.zoneRadius, abilityValues.zoneRadius);

        zoneTimer.ResetTimer(abilityValues.zoneTime);

        isInitialized = true;
    }

    void ManageZoneTime()
    {
        if (isInitialized)
        {
            if (zoneTimer.TimerIsDone())
            {
                Destroy(gameObject);
            }
        }
    }

    void DoEffectToTarget(GameObject target)
    {
        Debug.Log(target.name + " is inside the Zone!");
    }


    void OnTriggerStay(Collider other)
    {
        if (isInitialized)
        {
            if (other.gameObject.CompareTag("GolemRed") || other.gameObject.CompareTag("GolemBlue"))
            {
                DoEffectToTarget(other.gameObject);
            }      
        }
    }
}
