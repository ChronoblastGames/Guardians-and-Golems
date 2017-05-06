using System.Collections;
using UnityEngine;

public class GuardianCooldownManager : MonoBehaviour
{
    private GlobalVariables globalVariables;

    [Header("Global Cooldown Attributes")]
    private float globalCooldownTime;

    [Header("Ability Cooldown Attributes")]
    public float ability1Time = 0f;
    public float ability2Time = 0f;
    public float ability3Time = 0f;
    public float ability4Time = 0f;

    [HideInInspector]
    public float ability1StartTime = 0f;
    [HideInInspector]
    public float ability2StartTime = 0f;
    [HideInInspector]
    public float ability3StartTime = 0f;
    [HideInInspector]
    public float ability4StartTime = 0f;

    public bool isGlobalCooldownReady = true;

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

        globalCooldownTime = globalVariables.guardianGlobalCooldown;
    }

    public void QueueGlobalCooldown()
    {
        StartCoroutine(StartGlobalCooldown());
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

                ability1StartTime = Time.time;

                yield return new WaitForSeconds(ability1Time);

                isAbility1Ready = true;
                break;

            case 1:
                isAbility2Ready = false;

                ability2StartTime = Time.time;

                yield return new WaitForSeconds(ability2Time);

                isAbility2Ready = true;
                break;

            case 2:
                isAbility3Ready = false;

                ability3StartTime = Time.time;

                yield return new WaitForSeconds(ability3Time);

                isAbility3Ready = true;
                break;

            case 3:
                isAbility4Ready = false;

                ability4StartTime = Time.time;

                yield return new WaitForSeconds(ability4Time);

                isAbility4Ready = true;
                break;
        }
    }
}
