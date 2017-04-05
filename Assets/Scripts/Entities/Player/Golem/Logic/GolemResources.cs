using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffect
{
    NONE,
    STUN,
    SLOW,
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
    private UIManager UIManager;

    private TimerClass staggerTimer;

    private ParticleSystem hitParticles;

    [Header("Player Health Attributes")]
    public float currentHealth;
    public float maxHealth;
    public float temporaryHealth;

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

    public AnimationCurve knockBackCurve;

    public bool isSlowed = false;
    public bool isKnockedBack = false;
    public bool isBleeding = false;
    public bool isShielded = false;
    public bool isStunned = false;
    public bool isSilenced = false;

    [HideInInspector]
    public GolemDefense golemDefense;

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
        RegenerateMana();
        ManageStaggerTimer();
    }

    void InitializeValues()
    {
        golemPlayerController = GetComponent<GolemPlayerController>();

        globalVariables = GameObject.FindObjectOfType<GlobalVariables>();

        UIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

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
            currentHealth += healthRegenerationSpeed * Time.fixedDeltaTime;

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
            currentMana += manaRegenerationSpeed * Time.fixedDeltaTime;

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

    public bool CanCast(float spellManaCost, float spellHealthCost)
    {
        if (currentMana > spellManaCost && currentHealth > spellHealthCost)
        {
            currentMana -= spellManaCost;
            currentHealth -= spellHealthCost;
            return true;
        }
        else
        {
            Debug.Log(gameObject.name + "is out of Mana");
            return false;
        }
    }

    public void TakeDamage(float damageValue, DamageType damageType, StatusEffect statusEffect, float effectStrength, float effectTime, GameObject damagingObject)
    {
        float baseDefense = golemDefense.baseDefense;

        if (statusEffect != StatusEffect.NONE)
        {
            InflictStatusEffect(statusEffect, effectStrength, effectTime, damagingObject);
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
        if (temporaryHealth > 0)
        {
            float calculateDamage = temporaryHealth - damageValue;

            if (calculateDamage < 0)
            {
                statusEffectList.Remove(StatusEffect.SHIELD);

                StopShield();

                currentHealth -= Mathf.Abs(damageValue);
            }
            else
            {
                temporaryHealth -= damageValue;
            }

            hitParticles.Emit(100);
        }
        else if (currentHealth > damageValue)
        {
            currentHealth -= damageValue;
            hitParticles.Emit(100);
        }
        else
        {
            Die();
        }

        UIManager.RequestDamageText(damageValue, transform);
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

    public void InflictStatusEffect(StatusEffect statusEffect, float effectStrength, float effectTime, GameObject damagingObject)
    {
        switch(statusEffect)
        {
            case StatusEffect.BLEED:
                break;

            case StatusEffect.MANA_DRAIN:
                break;

            case StatusEffect.SHIELD:
                StartCoroutine(Shield(effectStrength, effectTime));

                statusEffectList.Add(StatusEffect.SHIELD);
                break;

            case StatusEffect.SILENCE:
                break;

            case StatusEffect.STUN:
                break;

            case StatusEffect.SLOW:
                StartCoroutine(Slow(effectStrength, effectTime));

                statusEffectList.Add(StatusEffect.SLOW);
                break;

            case StatusEffect.KNOCKBACK:
                Vector3 interceptVec = (damagingObject.transform.position - transform.position).normalized;
                interceptVec.y = 0;

                StartCoroutine(Knockback(-interceptVec, knockBackCurve, effectStrength, effectTime));

                statusEffectList.Add(StatusEffect.KNOCKBACK);
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


    IEnumerator Knockback(Vector3 interceptVec, AnimationCurve knockbackCurve, float knockbackStrength, float knockbackTime)
    {      
        float knockbackTimer = 0f;

        golemPlayerController.StopMovement();

        GetStaggered();

        golemPlayerController.canRotate = false;
        golemPlayerController.canUseAbilities = false;
        golemPlayerController.canBlock = false;
        golemPlayerController.canAttack = false;

        isKnockedBack = true;

        while (knockbackTimer <= knockbackTime)
        {
            knockbackTimer += Time.deltaTime / knockbackTime;

            float knockbackCurrentSpeed = knockbackStrength * knockbackCurve.Evaluate(knockbackTimer);

            golemPlayerController.characterController.Move((interceptVec * knockbackCurrentSpeed) * Time.deltaTime);

            yield return null;
        }

        StopKnockback();
    }

    void StopKnockback()
    {
        golemPlayerController.StartMovement();

        golemPlayerController.canRotate = true;
        golemPlayerController.canUseAbilities = true;
        golemPlayerController.canBlock = true;
        golemPlayerController.canAttack = true;

        isKnockedBack = false;

        statusEffectList.Remove(StatusEffect.KNOCKBACK);
    }

    IEnumerator Slow(float slowStrength, float slowTime)
    {
        isSlowed = true;

        golemPlayerController.movementSpeed = golemPlayerController.baseMovementSpeed / slowStrength;

        yield return new WaitForSeconds(slowTime);

        StopSlow();
    }

    void StopSlow()
    {
        isSlowed = false;

        golemPlayerController.movementSpeed = golemPlayerController.baseMovementSpeed;

        statusEffectList.Remove(StatusEffect.SLOW);
    }

    IEnumerator Shield (float shieldStrength, float shieldTime)
    {
        isShielded = true;

        temporaryHealth += shieldStrength;

        yield return new WaitForSeconds(shieldTime);

        StopShield();
    }

    void StopShield()
    {
        isShielded = false;

        temporaryHealth = 0;

        statusEffectList.Remove(StatusEffect.SHIELD);
    }
}


