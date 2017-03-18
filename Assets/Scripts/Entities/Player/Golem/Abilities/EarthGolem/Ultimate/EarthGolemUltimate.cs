using System.Collections;
using UnityEngine;

public class EarthGolemUltimate : GolemAbilityBase
{
    [Header("Ability Attributes")]
    public GameObject earthFragments;

    public float abilityRadius;
    public float ringCount;
    private int fragmentCountPerRing;

    private void Start()
    {
        InitializeAbility();
    }

    public override void InitializeAbility()
    {
        if (abilityValues.activeTime > 0)
        {
            Destroy(gameObject, abilityValues.activeTime);
        }

        fragmentCountPerRing = (int)abilityRadius * 2;

        isAbilityActive = true;

        SpawnShards();
    }

    void SpawnShards()
    {
        Vector3 centerVec = transform.position;

        float distanceBetweenShards = 360 / fragmentCountPerRing;

        for (int o = 1; o < ringCount + 1; o++)
        {
            float newRadius = abilityRadius + (o * ringCount);

            for (int i = 0; i < fragmentCountPerRing; i++)
            {
                Vector3 shardPos = CalculateCircle(transform.position, newRadius, distanceBetweenShards, i);
                float shardAngle = distanceBetweenShards * i;

                Quaternion shardRotation = Quaternion.Euler(-60, shardAngle, 0);
                GameObject newShard = Instantiate(earthFragments, shardPos, shardRotation, transform) as GameObject;
                newShard.layer = gameObject.layer;
                newShard.GetComponent<EarthFragments>().ablityValues = abilityValues;

            }
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
