﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirenatoController : AbilityCastBase
{
    private Projector spellAreaProjector;

    private List<GameObject> hitObjectList = new List<GameObject>();

    [Header("Ability Attributes")]
    public float rotationSpeed;

    [Header("Projector Values")]
    private float t = 0;

    public float projectorGrowSpeed = 0f;

    public float maxProjectorSize = 10f;

    public bool canIncreaseProjectorSize = false;

    public override void InitializeAbility()
    {
        spellAreaProjector = transform.GetChild(0).GetComponent<Projector>();

        if (abilityValues.teamColor == PlayerTeam.RED)
        {
            spellAreaProjector.material.color = Colors.YellowTeamColor;
        }
        else if (abilityValues.teamColor == PlayerTeam.BLUE)
        {
            spellAreaProjector.material.color = Colors.BlueTeamColor;
        }

        if (abilityValues.activeTime > 0)
        {
            Destroy(gameObject, abilityValues.activeTime);
        }

        isAbilityActive = true;

        canIncreaseProjectorSize = true;

        castAudio.Play();
    }

    private void FixedUpdate()
    {
        Rotate();

        LerpProjector();
    }

    void Rotate()
    {
        if (isAbilityActive)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.fixedDeltaTime);
        }
    }

    void LerpProjector()
    {
        if (canIncreaseProjectorSize)
        {
            float currentSize = spellAreaProjector.orthographicSize;

            spellAreaProjector.orthographicSize = Mathf.Lerp(currentSize, maxProjectorSize, t);

            t += Time.fixedDeltaTime / projectorGrowSpeed;

            if (spellAreaProjector.orthographicSize == maxProjectorSize)
            {
                canIncreaseProjectorSize = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isAbilityActive)
        {
            if (gameObject.layer == LayerMask.NameToLayer("GolemRed"))
            {
                if (other.gameObject.CompareTag("GolemBlue"))
                {
                    if (!hitObjectList.Contains(other.gameObject))
                    {
                        other.gameObject.GetComponent<GolemResources>().TakeDamage(abilityValues.damageAmount, abilityValues.damageType, abilityValues.statusEffect, abilityValues.effectStrength, abilityValues.effectTime, abilityValues.effectFrequency, gameObject, abilityValues.casterGameObject);
                        StartCoroutine(ManageDamage(abilityValues.damageFrequency, other.gameObject));
                    }
                }        
            }
            else if (gameObject.layer == LayerMask.NameToLayer("GolemBlue"))
            {
                if (other.gameObject.CompareTag("GolemRed"))
                {
                    if (!hitObjectList.Contains(other.gameObject))
                    {
                        other.gameObject.GetComponent<GolemResources>().TakeDamage(abilityValues.damageAmount, abilityValues.damageType, abilityValues.statusEffect, abilityValues.effectStrength, abilityValues.effectTime, abilityValues.effectFrequency, gameObject, abilityValues.casterGameObject);
                        StartCoroutine(ManageDamage(abilityValues.damageFrequency, other.gameObject));
                    }
                }       
            }
        }
    }

    private IEnumerator ManageDamage(float damageIncrement, GameObject hitObject)
    {
        hitObjectList.Add(hitObject);

        if (damageIncrement > 0)
        {
            yield return new WaitForSeconds(damageIncrement);
        }

        hitObjectList.Remove(hitObject);

        yield return null;
    }
}
