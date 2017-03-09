using System.Collections;
using UnityEngine;

public class EarthGolemAbility1 : GolemAbilityBase 
{
    private TimerClass activeTimer;

    [Header("Ability Attributes")]
    public GameObject earthShard;

    private SphereCollider abilityTrigger;

    public int shardCount;
    public int ringCount;

    public float minAbilityRadius;
    public float maxAbilityRadius;

    private float abilityRadius;

    public float shardDepth;

    private bool timerActive;

    private void Start()
    {
        InitializeAbility();
    }

    void FixedUpdate()
    {
        ManageTimer();
    }

    public override void InitializeAbility()
    {
        activeTimer = new TimerClass();

        if (abilityValues.holdTime < minAbilityRadius)
        {
            abilityValues.holdTime = minAbilityRadius;
            abilityRadius = abilityValues.holdTime;
        }
        else if (abilityValues.holdTime > maxAbilityRadius)
        {
            abilityValues.holdTime = maxAbilityRadius;
            abilityRadius = abilityValues.holdTime;
        }
        else
        {
            abilityRadius = abilityValues.holdTime;
        }

        if (abilityValues.activeTime > 0)
        {
            activeTimer.ResetTimer(abilityValues.activeTime);

            timerActive = true;
        }

        abilityTrigger = GetComponent<SphereCollider>();
        abilityTrigger.radius = abilityRadius * 1.75f;

        SpawnShards();
    }

    void SpawnShards()
    {
        Vector3 centerVec = transform.position;

        float distanceBetweenShards = 360 / shardCount;

        for (int o = 1; o < ringCount + 1; o++)
        {
            float newRadius = abilityRadius + (o * ringCount);
       
            for (int i = 0; i < shardCount; i++)
            {
                Vector3 shardPos = CalculateCircle(transform.position, newRadius, distanceBetweenShards, i);
                float shardAngle = distanceBetweenShards * i;

                Quaternion shardRotation = Quaternion.Euler(0, shardAngle, 0);
                GameObject newShard = Instantiate(earthShard, shardPos, shardRotation, transform) as GameObject;
                newShard.layer = gameObject.layer;

            }
        }
    }

    Vector3 CalculateCircle(Vector3 centerVector, float circleRadius, float distBetween, int interationCount)
    {
        float angle = distBetween * interationCount;
        Vector3 positionVec;

        positionVec.x = centerVector.x + circleRadius * Mathf.Sin(angle * Mathf.Deg2Rad);
        positionVec.z = centerVector.z + circleRadius * Mathf.Cos(angle * Mathf.Deg2Rad);
        positionVec.y = shardDepth;

        return positionVec;
    }

    void ManageTimer()
    {
        if (timerActive)
        {
            if (activeTimer.TimerIsDone())
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.layer == LayerMask.NameToLayer("GolemRed"))
        {
            if (other.CompareTag("GolemBlue"))
            {
                other.gameObject.GetComponent<GolemResources>().TakeDamage(abilityValues.damageAmount, abilityValues.damageType, abilityValues.statusEffect, abilityValues.effectStrength, gameObject);
            }
        }
        else if (gameObject.layer == LayerMask.NameToLayer("GolemBlue"))
        {
            if (other.CompareTag("GolemRed"))
            {
                other.gameObject.GetComponent<GolemResources>().TakeDamage(abilityValues.damageAmount, abilityValues.damageType, abilityValues.statusEffect, abilityValues.effectStrength, gameObject);
            }
        }
    }
}
