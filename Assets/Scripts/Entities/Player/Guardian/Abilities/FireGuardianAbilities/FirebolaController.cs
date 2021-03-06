﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebolaController : AbilityCastBase
{
    [Header("Fireball Attributes")]
    public GameObject fireballYellowPrefab;
    public GameObject fireballBluePrefab;

    private GameObject fireballGameObject;

    [Space(10)]
    public GameObject fireballHolder;

    public List<GameObject> fireballList;

    [Space(10)]
    public int fireballCount;
    public float fireballRadius;
    public float fireballSpawnHeight;

    [Header("Rotation Values")]
    public float rotationSpeed;

    public bool canMove;
    public bool canRotate;

    void FixedUpdate()
    {
        Movement();
    }

    public override void InitializeAbility()
    {
        SpawnFireballs();

        canMove = true;
        canRotate = true;

        if (abilityValues.activeTime > 0)
        {
            Destroy(gameObject, abilityValues.activeTime);
        }
    }

    void Movement()
    {
        if (canRotate)
        {
            fireballHolder.transform.Rotate(Vector3.up * Time.fixedDeltaTime * rotationSpeed, Space.World);
        }

        if (canMove)
        {
            transform.position += transform.forward * abilityValues.travelSpeed * Time.fixedDeltaTime;
        }

        if (isAbilityActive)
        {
            if (fireballList.Count == 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void SpawnFireballs()
    {
        Vector3 centerVec = transform.position;

        float distanceBetween = 360 / fireballCount;

        if (abilityValues.teamColor == PlayerTeam.RED)
        {
            fireballGameObject = fireballYellowPrefab;
        }
        else if (abilityValues.teamColor == PlayerTeam.BLUE)
        {
            fireballGameObject = fireballBluePrefab;
        }

        for (int i = 0; i < fireballCount; i++)
        {
            Vector3 fireballPos = CalculateCircle(transform.position, fireballRadius, distanceBetween, i);

            GameObject newFireball = Instantiate(fireballGameObject, fireballPos, Quaternion.identity, fireballHolder.transform);
            newFireball.GetComponent<FireOrbSubController>().fireballAbilityValues = abilityValues;
            newFireball.layer = gameObject.layer;

            fireballList.Add(newFireball);
        }

        isAbilityActive = true;
    }

    Vector3 CalculateCircle(Vector3 centerVector, float circleRadius, float distBetween, int interationCount)
    {
        float angle = distBetween * interationCount;
        Vector3 positionVec;

        positionVec.x = centerVector.x + circleRadius * Mathf.Sin(angle * Mathf.Deg2Rad);
        positionVec.z = centerVector.z + circleRadius * Mathf.Cos(angle * Mathf.Deg2Rad);
        positionVec.y = fireballSpawnHeight;

        return positionVec;
    }
}
