using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CooldownUI : MonoBehaviour 
{
    public GolemCooldownManager targetGolemCooldowns;
    public GuardianCooldownManager targetGuardianCooldowns;

    public PlayerType playerType;

    [Header("UI Elements")]
    public Image ability1CooldownImage;
    public Image ability2CooldownImage;
    public Image ability3CooldownImage;
    public Image ability4CooldownImage;

    private void Update()
    {
        ManageCooldownUI();
    }

    private void ManageCooldownUI()
    {
        if (playerType == PlayerType.GOLEM)
        {
           if (!targetGolemCooldowns.isAbility1Ready)
           {
                ability1CooldownImage.fillAmount = targetGolemCooldowns.ability1StartTime / Time.time;
           }

           if (!targetGolemCooldowns.isAbility2Ready)
           {
                ability2CooldownImage.fillAmount = targetGolemCooldowns.ability2StartTime / Time.time;

                Debug.Log(targetGolemCooldowns.ability2StartTime / Time.time);
            }

            if (!targetGolemCooldowns.isAbility3Ready)
           {
                ability3CooldownImage.fillAmount = targetGolemCooldowns.ability3StartTime / Time.time;
           }

           if (!targetGolemCooldowns.isAbility4Ready)
           {
                ability4CooldownImage.fillAmount = targetGolemCooldowns.ability4StartTime / Time.time;
           }
        }
        else if (playerType == PlayerType.GUARDIAN)
        {
            if (!targetGuardianCooldowns.isAbility1Ready)
            {
                ability1CooldownImage.fillAmount = targetGuardianCooldowns.ability1StartTime / Time.time;
            }

            if (!targetGuardianCooldowns.isAbility2Ready)
            {
                ability2CooldownImage.fillAmount = targetGuardianCooldowns.ability2StartTime / Time.time;
            }

            if (!targetGuardianCooldowns.isAbility3Ready)
            {
                ability3CooldownImage.fillAmount = targetGuardianCooldowns.ability3StartTime / Time.time;
            }

            if (!targetGuardianCooldowns.isAbility4Ready)
            {
                ability4CooldownImage.fillAmount = targetGuardianCooldowns.ability4StartTime / Time.time;
            }
        }
    }

    public void StartCooldownUI(int cooldownID)
    {

    }
}
