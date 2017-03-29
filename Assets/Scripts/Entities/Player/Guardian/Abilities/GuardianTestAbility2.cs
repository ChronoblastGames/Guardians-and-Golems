using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianTestAbility2 : AbilityCastBase
{
    [Header("Ability Attributes")]
    public GameObject fireballPrefab;

    public float abilityRadius;
    public float ringCount;
    public float abilityHeight;

    private int fragmentCountPerRing;

    public override void InitializeAbility()
    {
        if (abilityValues.activeTime > 0)
        {
            Destroy(gameObject, abilityValues.activeTime);
        }

        fragmentCountPerRing = (int)abilityRadius * 2;

        isAbilityActive = true;

        SpawnFireballs();
    }

    void SpawnFireballs()
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

                Quaternion shardRotation = Quaternion.Euler(0, shardAngle, 0);
                GameObject newFireball = Instantiate(fireballPrefab, shardPos, shardRotation, transform) as GameObject;
                newFireball.layer = gameObject.layer;

                newFireball.GetComponent<BaseProjectileAbility>().abilityValues = abilityValues;
                newFireball.GetComponent<BaseProjectileAbility>().InitializeAbility();
            }
        }
    }

    Vector3 CalculateCircle(Vector3 centerVector, float circleRadius, float distBetween, int interationCount)
    {
        float angle = distBetween * interationCount;
        Vector3 positionVec;

        positionVec.x = centerVector.x + circleRadius * Mathf.Sin(angle * Mathf.Deg2Rad);
        positionVec.z = centerVector.z + circleRadius * Mathf.Cos(angle * Mathf.Deg2Rad);
        positionVec.y = abilityHeight;

        return positionVec;
    }

}
