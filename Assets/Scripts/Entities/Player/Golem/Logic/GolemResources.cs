using System.Collections;
using System.Collections.Generic;
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
    NONE,
    STUN,
    BLEED,
    MANA_DRAIN,
    SILENCE,
    SHIELD,
    KNOCKBACK
}


public class GolemResources : MonoBehaviour
{
    private GolemPlayerController golemPlayerController;
    private GlobalVariables globalVariables;

    private TimerClass staggerTimer;

    private ParticleSystem hitParticles;

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

    [Header("Stagger Attributes")]
    public bool isStaggerTimerActive;

    private float staggerDamage;

    private bool isStaggerResist;

    [Header("Player Status Effect")]
    public List<StatusEffect> statusEffectList;

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
        ManageStatusEffect();
        ManageStaggerTimer();
    }

    void InitializeValues()
    {
        golemPlayerController = GetComponent<GolemPlayerController>();

        globalVariables = GameObject.FindObjectOfType<GlobalVariables>();

        staggerTimer = new TimerClass();

        hitParticles = GetComponent<ParticleSystem>();

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

    void ManageStatusEffect()
    {
        if (statusEffectList.Count > 0)
        {
            for (int i = 0; i < statusEffectList.Count; i++)
            {
                switch (statusEffectList[i])
                {
                    case StatusEffect.BLEED:
                        break;

                    case StatusEffect.MANA_DRAIN:
                        break;

                    case StatusEffect.SILENCE:
                        break;

                    case StatusEffect.STUN:
                        break;

                    case StatusEffect.SHIELD:
                        break;

                    case StatusEffect.KNOCKBACK:
                        break;

                    default:
                        Debug.Log("Incorrect Argument passed through Status Effect Manager");
                        break;
                }
            }
        }   
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

    public void TakeDamage(float damageValue, DamageType damageType, StatusEffect statusEffect, float effectStrength, GameObject damagingObject)
    {
        float baseDefense = golemDefense.baseDefense;

        if (golemPlayerController.isBlocking)
        {
            baseDefense *= 2f;
        }

        if (statusEffect != StatusEffect.NONE)
        {
            InflictStatusEffect(statusEffect, effectStrength, damagingObject);
        }

        float calculatedResistance;
        float calculatedDamage;

        DetermineStagger(damageValue);

        switch(damageType)
        {
            case DamageType.EARTH:
                calculatedResistance = baseDefense * golemDefense.earthDefense;
                calculatedDamage = damageValue / calculatedResistance;
                DealDamage(calculatedDamage, damagingObject, DamageType.EARTH);
                break;

            case DamageType.FIRE:
                calculatedResistance = baseDefense * golemDefense.fireDefense;
                calculatedDamage = damageValue / calculatedResistance;
                DealDamage(calculatedDamage, damagingObject, DamageType.FIRE);
                break;

            case DamageType.ICE:
                calculatedResistance = baseDefense * golemDefense.waterDefense;
                calculatedDamage = damageValue / calculatedResistance;
                DealDamage(calculatedDamage, damagingObject, DamageType.ICE);
                break;

            case DamageType.WIND:
                calculatedResistance = baseDefense * golemDefense.windDefense;
                calculatedDamage = damageValue / calculatedResistance;
                DealDamage(calculatedDamage, damagingObject, DamageType.WIND);
                break;

            case DamageType.PIERCE:
                calculatedResistance = baseDefense * golemDefense.pierceDefense;
                calculatedDamage = damageValue / calculatedResistance;
                DealDamage(calculatedDamage, damagingObject, DamageType.PIERCE);
                break;

            case DamageType.SLASH:
                calculatedResistance = baseDefense * golemDefense.slashDefense;
                calculatedDamage = damageValue / calculatedResistance;
                DealDamage(calculatedDamage, damagingObject, DamageType.SLASH);
                break;

            case DamageType.SMASH:
                calculatedResistance = baseDefense * golemDefense.smashDefense;
                calculatedDamage = damageValue / calculatedResistance;
                DealDamage(calculatedDamage, damagingObject, DamageType.SMASH);
                break;

            case DamageType.PURE:
                calculatedResistance = baseDefense;
                calculatedDamage = damageValue / calculatedResistance;
                DealDamage(calculatedDamage, damagingObject, DamageType.PURE);
                break;

            default:
                Debug.Log("Something went wrong in TakeDamage, wrong argument passed, was: " + damageType);
                break;
        }
    }

    public void DealDamage(float damageValue, GameObject damagingObject, DamageType damageType)
    {
        if (currentHealth > damageValue)
        {
            currentHealth -= damageValue;
            hitParticles.Emit(100);
        }
        else
        {
            Die();
        }
    }

    void ManageStaggerTimer()
    {
        if (staggerTimer.TimerIsDone())
        {
            staggerDamage = 0;
            isStaggerTimerActive = false;
        }


    }

    void DetermineStagger(float damageValue)
    {
        if (!golemPlayerController.isStaggered && !isStaggerResist)
        {
            if (isStaggerTimerActive)
            {
                staggerDamage += damageValue;

                if (staggerDamage > golemDefense.golemStability)
                {
                    GetStaggered();
                    staggerDamage = 0;
                }        
            }
            else
            {
                staggerTimer.ResetTimer(globalVariables.golemStaggerTime);
                isStaggerTimerActive = true;
                staggerDamage += damageValue;
            }
        }      
    }

    void GetStaggered()
    {
        golemPlayerController.Stagger();
    }

    public void InflictStatusEffect(StatusEffect statusEffect, float effectStrength, GameObject damagingObject)
    {
        Debug.Log(gameObject + " was inflicted with " + statusEffect + " for " + effectStrength + " by " + damagingObject.name);

        switch(statusEffect)
        {
            case StatusEffect.BLEED:
                break;

            case StatusEffect.MANA_DRAIN:
                break;

            case StatusEffect.SHIELD:
                break;

            case StatusEffect.SILENCE:
                break;

            case StatusEffect.STUN:
                break;

            case StatusEffect.KNOCKBACK:
                Vector3 interceptVec = (damagingObject.transform.position - transform.position).normalized;
                interceptVec.y = 0;
                interceptVec*= effectStrength;

                golemPlayerController.characterController.Move(interceptVec * Time.deltaTime);
                break;

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


