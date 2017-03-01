using System.Collections;
using UnityEngine;

public class EarthGolemAbility1 : GolemAbilityBase 
{
    private TimerClass activeTimer;

    [Header("Ability Attributes")]
    public GameObject earthShard;

    public int shardCount;
    public int ringCount;

    public float abilityRadius;

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

        SpawnShards();

        if (abilityValues.activeTime > 0)
        {
            activeTimer.ResetTimer(abilityValues.activeTime);

            timerActive = true;
        }
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
}
