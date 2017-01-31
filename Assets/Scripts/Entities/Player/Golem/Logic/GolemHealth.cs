using System.Collections;
using UnityEngine;

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
        
    }

    public void TakeDamage(float damageValue)
    {
        //Logic 
    }

    public void GetStunned()
    {
        //Debug I should be stunned
    }
}

public enum HealthStatus
{
    FULL_HEALTH,
    HEALTHY,
    INJURED,
    CRITICAL
}
