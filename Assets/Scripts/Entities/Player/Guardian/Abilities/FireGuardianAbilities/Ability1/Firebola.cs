using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firebola : AbilityCastBase
{
    [Header("Fireball Attributes")]
    public GameObject fireballPrefab;
    public GameObject fireballHolder;

    public List<GameObject> fireballList;

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
            transform.position += transform.forward * abilityValues.projectileSpeed * Time.fixedDeltaTime;
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

        for (int i = 0; i < fireballCount; i++)
        {
            Vector3 fireballPos = CalculateCircle(transform.position, fireballRadius, distanceBetween, i);

            GameObject newFireball = Instantiate(fireballPrefab, fireballPos, Quaternion.identity, fireballHolder.transform);
            newFireball.GetComponent<FireOrbController>().fireballAbilityValues = abilityValues;
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
