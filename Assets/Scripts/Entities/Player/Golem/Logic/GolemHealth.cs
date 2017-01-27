using System.Collections;
using UnityEngine;

public class GolemHealth : MonoBehaviour
{
    private GolemPlayerController golemPlayerController;

    [Header("Player Health Attributes")]
    public float currentHealth;
    public float maxHealth;

    void Awake()
    {
        golemPlayerController = GetComponent<GolemPlayerController>();

        InitializeValues();
    }

    void Update()
    {
        DetermineHealthStatus();
    }

    void InitializeValues()
    {
        maxHealth = golemPlayerController.baseHealth;
        currentHealth = maxHealth;
    }

    void DetermineHealthStatus()
    {
        
    }

    public void TakeDamage(float damageValue)
    {
        //Logic 
    }
}

public enum HealthStatus
{
    FULL_HEALTH,
    HEALTHY,
    INJURED,
    CRITICAL
}
