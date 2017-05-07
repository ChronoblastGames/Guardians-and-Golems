using System.Collections;
using UnityEngine;

public class GolemCooldownManager : MonoBehaviour 
{
    private GlobalVariables globalVariables;

    [Header("Global Cooldown Attributes")]
    private float globalCooldownTime;

    [Header("Ability Cooldown Attributes")]
    public float dodgeCooldownTime;

    [Header("Abilities Cooldowns")]
    public float ability1CooldownTime = 0f;
    public float ability2CooldownTime = 0f;
    public float ability3CooldownTime = 0f;
    public float ability4CooldownTime = 0f;

    [Header("Ability Cooldown End Time <<DONT EDIT>>")]
    public float ability1CooldownEndTime = 0f;
    public float ability2CooldownEndTime = 0f;
    public float ability3CooldownEndTime = 0f;
    public float ability4CooldownEndTime = 0f;

    [Header("Ability Booleans")]
    public bool isGlobalCooldownReady = true;
    public bool isDodgeCooldownReady = true;

    public bool isAbility1Ready = true;
    public bool isAbility2Ready = true;
    public bool isAbility3Ready = true;
    public bool isAbility4Ready = true;

    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        globalVariables = GameObject.FindObjectOfType<GlobalVariables>();

        globalCooldownTime = globalVariables.golemGlobalCooldown;
    }

    private void Update()
    {
        ManageCooldown();
    }

    private void ManageCooldown()
    {
        if (!isAbility1Ready)
        {
            ability1CooldownEndTime -= Time.deltaTime;
        }

        if (!isAbility2Ready)
        {
            ability2CooldownEndTime -= Time.deltaTime;
        }

        if (!isAbility3Ready)
        {
            ability3CooldownEndTime -= Time.deltaTime;
        }

        if (!isAbility4Ready)
        {
            ability4CooldownEndTime -= Time.deltaTime;
        }
    }

    public void QueueGlobalCooldown()
    {
        StartCoroutine(StartGlobalCooldown());
    }

    public void QueueDodgeCooldown()
    {
        StartCoroutine(StartDodgeCooldown());
    }

    public void QueueAbilityCooldown(int abilityNum)
    {
        StartCoroutine(StartAbilityCooldown(abilityNum));
    }

    public bool CanUseAbility(int abilityNum)
    {
        switch (abilityNum)
        {
            case 0:
                if (isAbility1Ready)
                {
                    return true;
                }
                else if (!isAbility1Ready)
                {
                    return false;
                }
                break;

            case 1:
                if (isAbility2Ready)
                {
                    return true;
                }
                else if (!isAbility2Ready)
                {
                    return false;
                }
                break;

            case 2:
                if (isAbility3Ready)
                {
                    return true;
                }
                else if (!isAbility3Ready)
                {
                    return false;
                }
                break;

            case 3:
                if (isAbility4Ready)
                {
                    return true;
                }
                else if (!isAbility4Ready)
                {
                    return false;
                }
                break;
        }

        return false;
    }

    public bool GlobalCooldownReady()
    {
        if (isGlobalCooldownReady)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool DodgeCooldownReady()
    {
        if (isDodgeCooldownReady)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator StartDodgeCooldown()
    {
        isDodgeCooldownReady = false;

        yield return new WaitForSeconds(dodgeCooldownTime);

        isDodgeCooldownReady = true;
    }

    private IEnumerator StartGlobalCooldown()
    {
        isGlobalCooldownReady = false;

        yield return new WaitForSeconds(globalCooldownTime);

        isGlobalCooldownReady = true;
    }

    private IEnumerator StartAbilityCooldown(int abilityNum)
    {
        switch (abilityNum)
        {
            case 0:
                isAbility1Ready = false;

                ability1CooldownEndTime = ability1CooldownTime;

                yield return new WaitForSeconds(ability1CooldownTime);

                isAbility1Ready = true;
                break;

            case 1:
                isAbility2Ready = false;

                ability2CooldownEndTime = ability2CooldownTime;

                yield return new WaitForSeconds(ability2CooldownTime);

                isAbility2Ready = true;
                break;

            case 2:
                isAbility3Ready = false;

                ability3CooldownEndTime = ability3CooldownTime;

                yield return new WaitForSeconds(ability3CooldownTime);

                isAbility3Ready = true;
                break;

            case 3:
                isAbility4Ready = false;

                ability4CooldownEndTime = ability4CooldownTime;

                yield return new WaitForSeconds(ability4CooldownTime);

                isAbility4Ready = true;
                break;
        }
    }
}
