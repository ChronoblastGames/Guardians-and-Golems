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

    private void ManageCooldownUI()
    {
        if (playerType == PlayerType.GOLEM)
        {
           if (!targetGolemCooldowns.isAbility1Ready)
           {

           }

           if (!targetGolemCooldowns.isAbility2Ready)
           {

           }

           if (!targetGolemCooldowns.isAbility3Ready)
           {

           }

           if (!targetGolemCooldowns.isAbility4Ready)
           {

           }
        }
        else if (playerType == PlayerType.GUARDIAN)
        {
            if (!targetGuardianCooldowns.isAbility1Ready)
            {

            }

            if (!targetGuardianCooldowns.isAbility2Ready)
            {

            }

            if (!targetGuardianCooldowns.isAbility3Ready)
            {

            }

            if (!targetGuardianCooldowns.isAbility4Ready)
            {

            }
        }
    }
}
