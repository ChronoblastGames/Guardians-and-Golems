using System.Collections;
using UnityEngine;

public class StatTracker : MonoBehaviour 
{
    [Header("Golem Game Stats")]
    public int redGolemKillingBlows;
    public int blueGolemKillingBlows;

    [Space(20)]

    public float redGolemTotalDamage;
    public float blueGolemTotalDamage;

    [Header("Guardian Game Stats")]
    public int redGuardianKillingBlows;
    public int blueGuardianKillingBlows;

    [Space(20)]
    public float redGuardianTotalDamage;
    public float blueGuardianTotalDamage;

    public void AddToKillingBlows(GameObject killerObject)
    {
        switch (killerObject.tag)
        {
            case "GolemRed":
                redGolemKillingBlows++;
                break;

            case "GolemBlue":
                blueGolemKillingBlows++;
                break;

            case "GuardianRed":
                redGuardianKillingBlows++;
                break;

            case "GuardianBlue":
                blueGuardianKillingBlows++;
                break;
        }
    }

    public void AddToTotalDamage(GameObject damageDealer, float damageValue)
    {
        switch (damageDealer.tag)
        {
            case "GolemRed":
                redGolemTotalDamage += damageValue;
                break;

            case "GolemBlue":
                blueGolemTotalDamage += damageValue;
                break;

            case "GuardianRed":
                redGuardianTotalDamage += damageValue;
                break;

            case "GuardianBlue":
                blueGuardianTotalDamage += damageValue;
                break;
        }
    }
}
