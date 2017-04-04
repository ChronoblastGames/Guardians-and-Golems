using System.Collections;
using UnityEngine;

public class FireOrbController : MonoBehaviour
{
    public AbilityValues fireballAbilityValues;

    private Firebola fireBola;

    void Start()
    {
        fireBola = transform.parent.parent.GetComponent<Firebola>();
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Hit Something");

        if (other.gameObject.layer == LayerMask.NameToLayer("Walls"))
        {
            fireBola.fireballList.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (gameObject.layer == LayerMask.NameToLayer("GolemRed"))
        {
            if (other.gameObject.CompareTag("GolemRed"))
            {
                other.gameObject.GetComponent<GolemResources>().TakeDamage(fireballAbilityValues.damageAmount, fireballAbilityValues.damageType, fireballAbilityValues.statusEffect, fireballAbilityValues.effectStrength, fireballAbilityValues.effectTime, gameObject);
                fireBola.fireballList.Remove(gameObject);
                Destroy(gameObject);
            }
        }
        else if (gameObject.layer == LayerMask.NameToLayer("GolemBlue"))
        {
            if (other.gameObject.CompareTag("GolemBlue"))
            {
                other.gameObject.GetComponent<GolemResources>().TakeDamage(fireballAbilityValues.damageAmount, fireballAbilityValues.damageType, fireballAbilityValues.statusEffect, fireballAbilityValues.effectStrength, fireballAbilityValues.effectTime, gameObject);
                fireBola.fireballList.Remove(gameObject);
                Destroy(gameObject);
            }
        }
    }
}
