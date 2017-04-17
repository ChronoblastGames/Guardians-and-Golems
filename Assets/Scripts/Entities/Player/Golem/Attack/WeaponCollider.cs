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
    private float statusEffectFrequency;

    private Collider weaponCol;

    private TrailRenderer trailRenderer;

    void Start()
    {
        InitializeDetection();
    }

    void InitializeDetection()
    {
        weaponCol = GetComponent<Collider>();

        trailRenderer = GetComponent<TrailRenderer>();
    }

    public void SetValues(DamageType attackType, float attackDamage, StatusEffect attackEffect, float effectStrength, float effectTime, float effectFrequency)
    {
        damageType = attackType;
        damageValue = attackDamage;
        statusEffect = attackEffect;
        statusEffectStrength = effectStrength;
        statusEffectTime = effectTime;
        statusEffectFrequency = effectFrequency;
    }

    public void EnableWeaponTrail()
    {
        trailRenderer.enabled = true;
    }

    public void DisableWeaponTrail()
    {
        trailRenderer.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (gameObject.layer == LayerMask.NameToLayer("GolemRed"))
        {
            if (other.gameObject.CompareTag("GolemBlue"))
            {
                other.gameObject.GetComponent<GolemResources>().TakeDamage(damageValue, damageType, statusEffect, statusEffectStrength, statusEffectTime, statusEffectFrequency, gameObject);
                weaponCol.enabled = false;
            }
        }
        else if (gameObject.layer == LayerMask.NameToLayer("GolemBlue"))
        {
            if (other.gameObject.CompareTag("GolemRed"))
            { 
                other.gameObject.GetComponent<GolemResources>().TakeDamage(damageValue, damageType, statusEffect, statusEffectStrength, statusEffectTime, statusEffectFrequency, gameObject);
                weaponCol.enabled = false;
            }
        } 
    }
}
