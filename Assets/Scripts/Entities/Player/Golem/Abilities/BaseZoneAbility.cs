using System.Collections;
using UnityEngine;

public class BaseZoneAbility : MonoBehaviour
{
    private TimerClass zoneTimer;

    [Header("Ability Attributes")]
    public AbilityValues abilityValues;

    [Header("Zone Visual")]
    private GameObject zoneVisual;

    [Header("Collision Attributes")]
    private Collider[] hitColliders;

    private bool isInitialized = false;

    public LayerMask targetMask;

    void Update()
    {
        ZoneCheck();
    }

    public void InitializeAbility()
    {
        zoneVisual = transform.GetChild(0).gameObject;

        zoneVisual.transform.localScale = new Vector3(abilityValues.zoneRadius, abilityValues.zoneRadius, abilityValues.zoneRadius);

        isInitialized = true;
    }

    void ZoneCheck()
    {
        if (isInitialized)
        {
            hitColliders = Physics.OverlapSphere(transform.position, abilityValues.zoneRadius, targetMask);

            for (int i = 0; i < hitColliders.Length; i++)
            {
                DoEffectToTarget(hitColliders[i].gameObject);
            }
        }   
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
        Debug.Log(target + " is inside the Zone!");
    }
}
