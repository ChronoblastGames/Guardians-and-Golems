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

    void Awake()
    {
        golemPlayerController = GetComponent<GolemPlayerController>();

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
        maxHealth = golemPlayerController.baseHealth;
        currentHealth = maxHealth;
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
        //Compare Damage Type with Defenses, applies calculated damage.
        switch(damageType)
        {
            case DamageType.EARTH:
                break;

            case DamageType.FIRE:
                break;

            case DamageType.ICE:
                break;

            case DamageType.PIERCE:
                break;

            case DamageType.SLASH:
                break;

            case DamageType.SMASH:
                break;

            case DamageType.NONE:
                break;
        }
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


