using System.Collections;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    [Header("Collider Attributes")]
    private DamageType damageType;

    private float damageValue;

    private StatusEffect statusEffect;
    private float statusEffectStrength;
    private float statusEffectTime;

    private Collider weaponCol;

    void Start()
    {
        InitializeDetection();
    }

    void InitializeDetection()
    {
        weaponCol = GetComponent<Collider>();
    }

    public void SetValues(DamageType attackType, float attackDamage, StatusEffect attackEffect, float effectStrength, float effectTime)
    {
        damageType = attackType;
        damageValue = attackDamage;
        statusEffect = attackEffect;
        statusEffectStrength = effectStrength;
        statusEffectTime = effectTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (gameObject.layer == LayerMask.NameToLayer("GolemRed"))
        {
            if (other.gameObject.CompareTag("GolemBlue"))
            {
                other.gameObject.GetComponent<GolemResources>().TakeDamage(damageValue, damageType, statusEffect, statusEffectStrength, statusEffectTime, gameObject);
                weaponCol.enabled = false;
            }
        }
        else if (gameObject.layer == LayerMask.NameToLayer("GolemBlue"))
        {
            if (other.gameObject.CompareTag("GolemRed"))
            { 
                other.gameObject.GetComponent<GolemResources>().TakeDamage(damageValue, damageType, statusEffect, statusEffectStrength, statusEffectTime, gameObject);
                weaponCol.enabled = false;
            }
        } 
    }
}
