using System.Collections;
using UnityEngine;

public class EarthGolemUltimate : AbilityCastBase
{
    [Header("Ability Attributes")]
    public GameObject earthFragmentsYellow;
    public GameObject earthFragmentsBlue;
    private GameObject earthFragments;

    public float abilityRadius;
    [Space(10)]
    public float shardDepth;
    public float ringCount;
    private int fragmentCountPerRing;

    public override void InitializeAbility()
    {
        if (abilityValues.activeTime > 0)
        {
            Destroy(gameObject, abilityValues.activeTime);
        }

        if (abilityValues.teamColor == PlayerTeam.RED)
        {
            earthFragments = earthFragmentsYellow;
        }
        else if (abilityValues.teamColor == PlayerTeam.BLUE)
        {
            earthFragments = earthFragmentsBlue;
        }

        fragmentCountPerRing = (int)abilityRadius * 2;

        SpawnShards();

        isAbilityActive = true;
    }

    void SpawnShards()
    {
        Vector3 centerVec = transform.position;

        float distanceBetweenShards = 360 / fragmentCountPerRing;

        for (int o = 0; o < ringCount; o++)
        {
            float newRadius = abilityRadius + (o * ringCount);

            for (int i = 0; i < fragmentCountPerRing; i++)
            {
                Vector3 shardPos = CalculateCircle(transform.position, newRadius, distanceBetweenShards, i);
                float shardAngle = distanceBetweenShards * i;

                Quaternion shardRotation = Quaternion.Euler(-80, shardAngle + 180f, 0);
                GameObject newShard = Instantiate(earthFragments, shardPos, shardRotation, transform) as GameObject;
                newShard.layer = gameObject.layer;       
                newShard.GetComponent<EarthFragments>().abilityValues = abilityValues;        
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
