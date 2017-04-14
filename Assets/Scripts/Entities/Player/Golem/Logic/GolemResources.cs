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
    KNOCKBACK,
    HEALOVERTIME,
    STAGGER,
    PULL
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

    [Header("Player Health Regeneration Attributes")]
    public float healthRegenerationSpeed;

    public bool canRegenerateHealth = true;

    [Header("Stagger Attributes")]
    public bool isStaggerTimerActive;

    private float staggerDamage;

    private bool isStaggerResist;

    [Header("Player Status Effect")]
    public List<StatusEffect> statusEffectList;

    public AnimationCurve knockBackCurve;
    public AnimationCurve pullCurve;
    [Space(10)]

    public bool isSlowed = false;
    public bool isKnockedBack = false;
    public bool isBleeding = false;
    public bool isShielded = false;
    public bool isStunned = false;
    public bool isSilenced = false;
    public bool isHealingOverTime = false;
    public bool isPulled = false;

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

        golemDefense = golemPlayerController.golemDefenseValues;
    }

    void RegenerateHealth()
    {
        if (currentHealth < maxHealth && canRegenerateHealth)
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

    void DetermineHealthStatus()
    {
        if (currentHealth < 0 && !golemPlayerController.isDead)
        {
            Die();
        }
    }

    public bool CanCast(float spellHealthCost)
    {
        if (spellHealthCost > 0)
        {
            if (currentHealth > spellHealthCost)
            {
                currentHealth -= spellHealthCost;
                return true;
            }
            else
            {
                Debug.Log(gameObject.name + "can't Cast");
                return false;
            }
        }
        else
        {
            return true;
        }    
    }

    public void TakeDamage(float damageValue, DamageType damageType, StatusEffect statusEffect, float effectStrength, float effectTime, float effectFrequency, GameObject damagingObject)
    {
        if (!golemPlayerController.isDead)
        {
            float baseDefense = golemDefense.baseDefense;

            if (statusEffect != StatusEffect.NONE)
            {
                InflictStatusEffect(statusEffect, effectStrength, effectTime, effectFrequency, damagingObject);
            }

            float calculatedResistance;
            float calculatedDamage;

            DetermineStagger(damageValue);

            switch (damageType)
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
            currentHealth = 0;
            Die();
        }

        UIManager.RequestDamageText(damageValue, transform, FloatingDamageTextType.DAMAGE);
    }

    public void GetHealed(float healAmount, StatusEffect statusEffect, float effectStrength, float effectTime, float effectFrequency, GameObject healingObject)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += healAmount;
            UIManager.RequestDamageText(healAmount, transform, FloatingDamageTextType.HEAL);
        }

        if (statusEffect != StatusEffect.NONE)
        {
            InflictStatusEffect(statusEffect, effectStrength, effectTime, effectFrequency, healingObject);
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

                    UIManager.RequestStatusText(0, transform, StatusEffect.STAGGER);
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

    void Die()
    {
        golemPlayerController.Die();

        canRegenerateHealth = false;
    }

    public void InflictStatusEffect(StatusEffect statusEffect, float effectStrength, float effectTime, float effectFrequency, GameObject damagingObject)
    {
        switch(statusEffect)
        {
            case StatusEffect.BLEED:
                StartCoroutine(Bleed(effectStrength, effectTime, effectFrequency));

                statusEffectList.Add(StatusEffect.BLEED);

                UIManager.RequestStatusText(effectStrength, transform, StatusEffect.BLEED);
                break;

            case StatusEffect.MANA_DRAIN:
                break;

            case StatusEffect.SHIELD:
                StartCoroutine(Shield(effectStrength, effectTime, effectFrequency));

                statusEffectList.Add(StatusEffect.SHIELD);

                UIManager.RequestStatusText(effectStrength, transform, StatusEffect.SHIELD);
                break;

            case StatusEffect.SILENCE:
                break;

            case StatusEffect.STUN:
                StartCoroutine(Stun(effectTime));

                statusEffectList.Add(StatusEffect.STUN);

                UIManager.RequestStatusText(effectStrength, transform, StatusEffect.STUN);
                break;

            case StatusEffect.SLOW:
                StartCoroutine(Slow(effectStrength, effectTime));

                statusEffectList.Add(StatusEffect.SLOW);

                UIManager.RequestStatusText(effectStrength, transform, StatusEffect.SLOW);
                break;

            case StatusEffect.KNOCKBACK:
                Vector3 interceptVec = (damagingObject.transform.position - transform.position).normalized;
                interceptVec.y = 0;

                StartCoroutine(Knockback(-interceptVec, knockBackCurve, effectStrength, effectTime));

                statusEffectList.Add(StatusEffect.KNOCKBACK);

                UIManager.RequestStatusText(effectStrength, transform, StatusEffect.KNOCKBACK);
                break;

            case StatusEffect.HEALOVERTIME:
                StartCoroutine(HealOverTime(effectStrength, effectTime, effectFrequency));

                statusEffectList.Add(StatusEffect.HEALOVERTIME);

                UIManager.RequestStatusText(effectStrength, transform, StatusEffect.HEALOVERTIME);
                break;

            case StatusEffect.PULL:
                Vector3 pullVec = (damagingObject.transform.position - transform.position).normalized;
                pullVec.y = 0;

                StartCoroutine(Pull(-pullVec, pullCurve, effectStrength, effectTime));
                statusEffectList.Add(StatusEffect.PULL);

                UIManager.RequestStatusText(effectStrength, transform, StatusEffect.PULL);
                break;

            default:
                Debug.Log("Something went wrong in InflictStatusEffect, wrong argument passed, was: " + statusEffect);
                break;
        }
    }

    IEnumerator Knockback(Vector3 interceptVec, AnimationCurve knockbackCurve, float knockbackStrength, float knockbackTime)
    {      
        float knockbackTimer = 0f;

        golemPlayerController.StopMovement();

        GetStaggered();

        golemPlayerController.canRotate = false;
        golemPlayerController.canUseAbilities = false;
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

        yield return null;
    }

    void StopKnockback()
    {
        golemPlayerController.StartMovement();

        golemPlayerController.canRotate = true;
        golemPlayerController.canUseAbilities = true;
        golemPlayerController.canAttack = true;

        isKnockedBack = false;

        statusEffectList.Remove(StatusEffect.KNOCKBACK);
    }

    IEnumerator Pull (Vector3 interceptVec, AnimationCurve pullCurve, float pullStrength, float pullTime)
    {
        float pullTimer = 0f;

        golemPlayerController.StopMovement();

        GetStaggered();

        golemPlayerController.canRotate = false;
        golemPlayerController.canUseAbilities = false;
        golemPlayerController.canAttack = false;

        isPulled = true;

        while (pullTimer <= pullTime)
        {
            pullTimer += Time.deltaTime / pullTime;

            float pullCurrentSpeed = pullStrength * pullCurve.Evaluate(pullTimer);

            golemPlayerController.characterController.Move((-interceptVec * pullCurrentSpeed) * Time.deltaTime);

            yield return null;
        }

        StopPull();

        yield return null;
    }

    void StopPull()
    {
        golemPlayerController.StartMovement();

        golemPlayerController.canRotate = true;
        golemPlayerController.canUseAbilities = true;
        golemPlayerController.canAttack = true;

        isPulled = false;

        statusEffectList.Remove(StatusEffect.PULL);
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

    IEnumerator Shield (float shieldStrength, float shieldTime, float shieldFrequency)
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

    IEnumerator Bleed (float bleedStrength, float bleedTime, float bleedFrequency)
    {
        float totalBleedTime = Time.time + bleedTime;
        float bleedCounts = (int)(bleedTime / bleedFrequency);

        if (Time.time < totalBleedTime)
        {
            for (int i = 0; i < bleedCounts; i++)
            {
                currentHealth -= bleedStrength;

                UIManager.RequestDamageText(bleedStrength, transform, FloatingDamageTextType.DAMAGE);

                yield return new WaitForSeconds(bleedFrequency);
            }
        }

        StopBleed();

        yield return null;
    }

    void StopBleed()
    {
        isBleeding = false;

        statusEffectList.Remove(StatusEffect.BLEED);
    }

    IEnumerator HealOverTime(float healStrength, float healTime, float healFrequency)
    {
        float totalHealTime = Time.time + healTime;
        int healCounts = (int)(healTime / healFrequency);

        if (Time.time < totalHealTime)
        {
            for (int i = 0; i < healCounts; i++)
            {
                if (currentHealth < maxHealth)
                {
                    currentHealth += healStrength;

                    UIManager.RequestDamageText(healStrength, transform, FloatingDamageTextType.HEAL);
                }
          
                yield return new WaitForSeconds(healFrequency);
            }
        }

        StopHealingOverTime();

        yield return null;
    }

    void StopHealingOverTime()
    {
        isHealingOverTime = false;

        statusEffectList.Remove(StatusEffect.HEALOVERTIME);
    }

    IEnumerator Stun(float stunTime)
    {
        golemPlayerController.StopMovement();

        yield return new WaitForSeconds(stunTime);

        golemPlayerController.StartMovement();

        StopStun();

        yield return null;
    }

    void StopStun()
    {
        statusEffectList.Remove(StatusEffect.STUN);
    }

}


