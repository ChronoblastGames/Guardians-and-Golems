﻿using System.Collections;
using UnityEngine;

public class EarthFragments : MonoBehaviour
{
    public AbilityValues ablityValues;

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.layer == LayerMask.NameToLayer("GolemRed"))
        {
            if (other.gameObject.CompareTag("GolemBlue"))
            {
                other.gameObject.GetComponent<GolemResources>().TakeDamage(ablityValues.damageAmount, ablityValues.damageType, ablityValues.statusEffect, ablityValues.effectStrength, ablityValues.effectTime, ablityValues.effectFrequency, gameObject);
                Destroy(gameObject);
            }
        }
        else if (gameObject.layer == LayerMask.NameToLayer("GolemBlue"))
        {
            if (other.gameObject.CompareTag("GolemRed"))
            {
                other.gameObject.GetComponent<GolemResources>().TakeDamage(ablityValues.damageAmount, ablityValues.damageType, ablityValues.statusEffect, ablityValues.effectStrength, ablityValues.effectTime, ablityValues.effectFrequency, gameObject);
                Destroy(gameObject);
            }
        }
    }
}
