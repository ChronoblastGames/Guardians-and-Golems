using System.Collections;
using UnityEngine;

public class EarthGolemUltimate : AbilityCastBase
{
    [Header("Ability Attributes")]
    public GameObject earthFragments;

    public float abilityRadius;
    public float ringCount;
    private int fragmentCountPerRing;

    [Header("Projector Attributes")]
    private Projector abilityProjector;

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

        abilityProjector = transform.GetChild(0).GetComponent<Projector>();

        fragmentCountPerRing = (int)abilityRadius * 2;

        SpawnShards();

        EnableProjector();

        isAbilityActive = true;
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

    void EnableProjector()
    {
        if (abilityValues.teamColor == PlayerTeam.RED)
        {
            abilityProjector.orthographicSize = abilityRadius * 5;

            abilityProjector.material.color = Color.yellow;
            abilityProjector.enabled = true;
        }
        else if (abilityValues.teamColor == PlayerTeam.BLUE)
        {
            abilityProjector.orthographicSize = abilityRadius * 5;

            abilityProjector.material.color = Color.blue;
            abilityProjector.enabled = true;
        }
    }

}
