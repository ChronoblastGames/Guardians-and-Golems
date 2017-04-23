using System.Collections;
using UnityEngine;

public class EarthShatter : AbilityCastBase 
{
    private TimerClass activeTimer;

    [Header("Ability Attributes")]
    public GameObject earthShardYellow;
    public GameObject earthShardBlue;

    private GameObject earthShard;

    private SphereCollider abilityTrigger;

    public float activeTime;

    public int shardCount;
    public int ringCount;

    public float abilityRadius;

    public float shardDepth;

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

        if (abilityValues.activeTime > 0)
        {
            activeTimer.ResetTimer(abilityValues.activeTime);

            isAbilityActive = true;
        }

        if (abilityValues.teamColor == PlayerTeam.RED)
        {
            earthShard = earthShardYellow;
        }
        else if (abilityValues.teamColor == PlayerTeam.BLUE)
        {
            earthShard = earthShardBlue;

        }

        abilityTrigger = GetComponent<SphereCollider>();
        abilityTrigger.radius = abilityRadius * 1.75f;
        shardCount = (int)abilityRadius * 2;

        SpawnShards();

        StartCoroutine(ActiveTimer(activeTime));
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

                Quaternion shardRotation = Quaternion.Euler(-60, shardAngle, 0);
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
        if (isAbilityActive)
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
                other.gameObject.GetComponent<GolemResources>().TakeDamage(abilityValues.damageAmount, abilityValues.damageType, abilityValues.statusEffect, abilityValues.effectStrength, abilityValues.effectTime, abilityValues.effectFrequency, gameObject, abilityValues.casterGameObject);
            }
        }
        else if (gameObject.layer == LayerMask.NameToLayer("GolemBlue"))
        {
            if (other.CompareTag("GolemRed"))
            {
                other.gameObject.GetComponent<GolemResources>().TakeDamage(abilityValues.damageAmount, abilityValues.damageType, abilityValues.statusEffect, abilityValues.effectStrength, abilityValues.effectTime, abilityValues.effectFrequency, gameObject, abilityValues.casterGameObject);
            }
        }
    }

    private IEnumerator ActiveTimer(float timeActive)
    {
        yield return new WaitForSeconds(timeActive);

        abilityTrigger.enabled = false;

        yield return null;
    }
}
