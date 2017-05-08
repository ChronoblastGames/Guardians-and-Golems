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
                ability1CooldownImage.fillAmount = targetGolemCooldowns.ability1CooldownEndTime / targetGolemCooldowns.ability1CooldownTime;
            }

            if (!targetGolemCooldowns.isAbility2Ready)
            {
                ability2CooldownImage.fillAmount = targetGolemCooldowns.ability2CooldownEndTime / targetGolemCooldowns.ability2CooldownTime;
            }

            if (!targetGolemCooldowns.isAbility3Ready)
            {
                ability3CooldownImage.fillAmount = targetGolemCooldowns.ability3CooldownEndTime / targetGolemCooldowns.ability3CooldownTime;
            }

            if (!targetGolemCooldowns.isAbility4Ready)
            {
                ability4CooldownImage.fillAmount = targetGolemCooldowns.ability4CooldownEndTime / targetGolemCooldowns.ability4CooldownTime;
            }
        }
        else if (playerType == PlayerType.GUARDIAN)
        {
            if (!targetGuardianCooldowns.isAbility1Ready)
            {
                ability1CooldownImage.fillAmount = targetGuardianCooldowns.ability1CooldownEndTime / targetGuardianCooldowns.ability1CooldownTime;
            }

            if (!targetGuardianCooldowns.isAbility2Ready)
            {
                ability2CooldownImage.fillAmount = targetGuardianCooldowns.ability2CooldownEndTime / targetGuardianCooldowns.ability2CooldownTime;
            }

            if (!targetGuardianCooldowns.isAbility3Ready)
            {
                ability3CooldownImage.fillAmount = targetGuardianCooldowns.ability3CooldownEndTime / targetGuardianCooldowns.ability3CooldownTime;
            }

            if (!targetGuardianCooldowns.isAbility4Ready)
            {
                ability4CooldownImage.fillAmount = targetGuardianCooldowns.ability4CooldownEndTime / targetGuardianCooldowns.ability4CooldownTime;
            }
        }
    }
}
