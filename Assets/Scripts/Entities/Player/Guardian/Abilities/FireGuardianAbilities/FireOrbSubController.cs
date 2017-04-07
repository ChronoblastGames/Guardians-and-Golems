using System.Collections;
using UnityEngine;

public class FireOrbSubController : MonoBehaviour
{
    public AbilityValues fireballAbilityValues;

    private FirebolaController fireBola;

    void Start()
    {
        fireBola = transform.parent.parent.GetComponent<FirebolaController>();
    }

    void OnCollisionEnter(Collision other)
    {
        fireBola.fireballList.Remove(gameObject);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (gameObject.layer == LayerMask.NameToLayer("GolemRed"))
        {
            if (other.gameObject.CompareTag("GolemBlue"))
            {
                other.gameObject.GetComponent<GolemResources>().TakeDamage(fireballAbilityValues.damageAmount, fireballAbilityValues.damageType, fireballAbilityValues.statusEffect, fireballAbilityValues.effectStrength, fireballAbilityValues.effectTime, fireballAbilityValues.effectFrequency, gameObject);
                fireBola.fireballList.Remove(gameObject);
                Destroy(gameObject);
            }
        }
        else if (gameObject.layer == LayerMask.NameToLayer("GolemBlue"))
        {
            if (other.gameObject.CompareTag("GolemRed"))
            {
                other.gameObject.GetComponent<GolemResources>().TakeDamage(fireballAbilityValues.damageAmount, fireballAbilityValues.damageType, fireballAbilityValues.statusEffect, fireballAbilityValues.effectStrength, fireballAbilityValues.effectTime, fireballAbilityValues.effectFrequency, gameObject);
                fireBola.fireballList.Remove(gameObject);
                Destroy(gameObject);
            }
        }
    }
}
