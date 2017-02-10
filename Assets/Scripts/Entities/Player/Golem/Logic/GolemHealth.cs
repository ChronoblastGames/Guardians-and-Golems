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

public class GolemHealth : MonoBehaviour
{
    private GolemPlayerController golemPlayerController;

    [Header("Player Health Attributes")]
    public float currentHealth;
    public float maxHealth;

    [Header("Player Health Regeneration Attributes")]
    public float healthRegenerationSpeed;

    [Header("Player Defensive Attributes")]
    public GolemDefense golemDefense;

    public float baseDefense {get {return golemDefense.baseDefense;}}

    void Awake()
    {     
        InitializeValues();
    }

    void Update()
    {
        DetermineHealthStatus();
    }

    private void FixedUpdate()
    {
        RegenerateHealth();
    }

    void InitializeValues()
    {
        golemPlayerController = GetComponent<GolemPlayerController>();
        maxHealth = golemPlayerController.baseHealth;
        currentHealth = maxHealth;
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
        }
    }

    void DetermineHealthStatus()
    {
        if (currentHealth < 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damageValue, DamageType damageType)
    {
        float calculatedResistance;
        float calculatedDamage;

        //Compare Damage Type with Defenses, applies calculated damage.
        switch(damageType)
        {
            case DamageType.EARTH:
                calculatedResistance = golemDefense.baseDefense * golemDefense.earthDefense;
                calculatedDamage = damageValue / calculatedResistance;
                DealDamage(calculatedDamage);
                break;

            case DamageType.FIRE:
                calculatedResistance = golemDefense.baseDefense * golemDefense.fireDefense;
                calculatedDamage = damageValue / calculatedResistance;
                DealDamage(calculatedDamage);
                break;

            case DamageType.ICE:
                calculatedResistance = golemDefense.baseDefense * golemDefense.waterDefense;
                calculatedDamage = damageValue / calculatedResistance;
                DealDamage(calculatedDamage);
                break;

            case DamageType.PIERCE:
                calculatedResistance = golemDefense.baseDefense * golemDefense.pierceDefense;
                calculatedDamage = damageValue / calculatedResistance;
                DealDamage(calculatedDamage);
                break;

            case DamageType.SLASH:
                calculatedResistance = golemDefense.baseDefense * golemDefense.slashDefense;
                calculatedDamage = damageValue / calculatedResistance;
                DealDamage(calculatedDamage);
                break;

            case DamageType.SMASH:
                calculatedResistance = golemDefense.baseDefense * golemDefense.smashDefense;
                calculatedDamage = damageValue / calculatedResistance;
                DealDamage(calculatedDamage);
                break;

            case DamageType.NONE:
                calculatedResistance = golemDefense.baseDefense;
                calculatedDamage = damageValue / calculatedResistance;
                DealDamage(calculatedDamage);
                break;
        }
    }

    public void DealDamage(float damageValue)
    {
        Debug.Log(damageValue);
    }

    public void GetStunned()
    {
        //Debug I should be stunned
    }

    void Die()
    {
        Debug.Log("I died " + gameObject.name);
    }
}


