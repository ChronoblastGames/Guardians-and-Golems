using System.Collections;
using UnityEngine;

public class EarthGolemAbility1 : GolemAbilityBase 
{
    [Header("Ability Attributes")]
    public GameObject earthShard;

    public int shardCount;

    public float abilityRadius;

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

        for (int i = 0; i < shardCount; i++)
        {
            Vector3 shardPos = CalculateCircle(transform.position, abilityRadius, distanceBetweenShards, i);
            float shardAngle = distanceBetweenShards * i;

            Quaternion shardRotation = Quaternion.Euler(0, shardAngle, 0);

            GameObject newShard = Instantiate(earthShard, shardPos, shardRotation, transform) as GameObject;
        }
    }

    Vector3 CalculateCircle(Vector3 centerVector, float circleRadius, float distBetween, int interationCount)
    {
        float angle = distBetween * interationCount;
        Vector3 positionVec;

        positionVec.x = centerVector.x + circleRadius * Mathf.Sin(angle * Mathf.Deg2Rad);
        positionVec.z = centerVector.z + circleRadius * Mathf.Cos(angle * Mathf.Deg2Rad);
        positionVec.y = 0;

        return positionVec;
    }
}
