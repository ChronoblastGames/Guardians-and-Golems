using System.Collections;
using UnityEngine;

public class EarthGolemAbility1 : GolemAbilityBase 
{
    [Header("Ability Attributes")]
    public GameObject earthShard;

    public int shardCount;
    public int ringCount;

    public float abilityRadius;

    public float shardDepth;

    private void Start()
    {
        InitializeAbility();
    }

    public override void InitializeAbility()
    {
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
}
