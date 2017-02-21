using System.Collections;
using UnityEngine;

public enum HealthStatus
{
    FULL,
    HEALTHY,
    INJURED,
    CRITICAL,
    DEAD
}

public enum ManaStatus
{
    FULL,
    HIGH,
    MEDIUM,
    LOW,
    NO_MANA
}

public enum StatusEffect
{
    STUN,
    BLEED,
    MANA_DRAIN,
    SILENCE
}


public class GolemResources : MonoBehaviour
{
    private GolemPlayerController golemPlayerController;

    [Header("Player Health Attributes")]
    public float currentHealth;
    public float maxHealth;

    [Header("Player Mana Attributes")]
    public float currentMana;
    public float maxMana;

    [Header("Player Health Regeneration Attributes")]
    public float healthRegenerationSpeed;

    [Header("Player Mana Regeneration Attributes")]
    public float manaRegenerationSpeed;

    [HideInInspector]
    public GolemDefense golemDefense;

    void Awake()
    {     
        InitializeValues();
    }

    void Update()
    {
        DetermineHealthStatus();
        DetermineManaStatus();
    }

    private void FixedUpdate()
    {
        RegenerateHealth();
        RegenerateMana();
    }

    void InitializeValues()
    {
        golemPlayerController = GetComponent<GolemPlayerController>();

        currentHealth = maxHealth;

        currentMana = maxMana;

        golemDefense = golemPlayerController.golemDefenseValues;
    }

    void RegenerateHealth()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += healthRegenerationSpeed * Time.deltaTime;

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }     
            else if (currentHealth < 0)
            {
                currentHealth = 0;
            }    
        }
    }

    void RegenerateMana()
    {
        if (currentMana < maxMana)
        {
            currentMana += manaRegenerationSpeed * Time.deltaTime;

            if (currentMana > maxMana)
            {
                currentMana = maxMana;
            }
            else if (currentMana < 0)
            {
                currentMana = 0;
            }
        }
    }

    void DetermineHealthStatus()
    {
        if (currentHealth < 0)
        {
            Die();
        }
    }

    void DetermineManaStatus()
    {

    }

    public bool CanCast(float spellManaCost)
    {
        if (currentMana > spellManaCost)
        {
            currentMana -= spellManaCost;
            return true;
        }
        else
        {
            Debug.Log(gameObject.name + "is out of Mana");
            return false;
        }
    }

    public void TakeDamage(float damageValue, DamageType damageType)
    {
        float baseDefense = golemDefense.baseDefense;

        if (golemPlayerController.isBlocking)
        {
            baseDefense *= 2f;
        }

        float calculatedResistance;
        float calculatedDamage;

        switch(damageType)
        {
            case DamageType.EARTH:
                calculatedResistance = baseDefense * golemDefense.earthDefense;
                calculatedDamage = damageValue / calculatedResistance;
                DealDamage(calculatedDamage);
                break;

            case DamageType.FIRE:
                calculatedResistance = baseDefense * golemDefense.fireDefense;
                calculatedDamage = damageValue / calculatedResistance;
                DealDamage(calculatedDamage);
                break;

            case DamageType.ICE:
                calculatedResistance = baseDefense * golemDefense.waterDefense;
                calculatedDamage = damageValue / calculatedResistance;
                DealDamage(calculatedDamage);
                break;

            case DamageType.WIND:
                calculatedResistance = baseDefense * golemDefense.windDefense;
                calculatedDamage = damageValue / calculatedResistance;
                DealDamage(calculatedDamage);
                break;

            case DamageType.PIERCE:
                calculatedResistance = baseDefense * golemDefense.pierceDefense;
                calculatedDamage = damageValue / calculatedResistance;
                DealDamage(calculatedDamage);
                break;

            case DamageType.SLASH:
                calculatedResistance = baseDefense * golemDefense.slashDefense;
                calculatedDamage = damageValue / calculatedResistance;
                DealDamage(calculatedDamage);
                break;

            case DamageType.SMASH:
                calculatedResistance = baseDefense * golemDefense.smashDefense;
                calculatedDamage = damageValue / calculatedResistance;
                DealDamage(calculatedDamage);
                break;

            case DamageType.PURE:
                calculatedResistance = baseDefense;
                calculatedDamage = damageValue / calculatedResistance;
                DealDamage(calculatedDamage);
                break;

            default:
                Debug.Log("Something went wrong in TakeDamage, wrong argument passed, was: " + damageType);
                break;
        }
    }

    public void DealDamage(float damageValue)
    {
        if (currentHealth > damageValue)
        {
            currentHealth -= damageValue;
            Debug.Log(gameObject.name + " took " + damageValue + " of damage");
        }
        else
        {
            Die();
        }
    }

    public void InflictStatusEffect(StatusEffect statusEffect)
    {
        switch(statusEffect)
        {
            default:
                Debug.Log("Something went wrong in InflictStatusEffect, wrong argument passed, was: " + statusEffect);
                break;
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " is Dead!");
        gameObject.SetActive(false);
    }
}


