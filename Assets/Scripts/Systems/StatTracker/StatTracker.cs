using System.Collections;
using UnityEngine;

public class StatTracker : MonoBehaviour 
{
    private UIManager UI;

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

    private void Start()
    {
        UI = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }

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

    public void InformationPassthrough(PlayerTeam losingTeam)
    {
        PlayerTeam winningTeam = PlayerTeam.NONE;

        PlayerType yellowTeamMVP = PlayerType.NONE;
        PlayerType blueTeamMVP = PlayerType.NONE;

        if (losingTeam == PlayerTeam.RED)
        {
            winningTeam = PlayerTeam.BLUE;
        }
        else if (losingTeam == PlayerTeam.BLUE)
        {
            winningTeam = PlayerTeam.RED;
        }

        if (redGolemKillingBlows > redGuardianKillingBlows)
        {
            yellowTeamMVP = PlayerType.GOLEM;
        }
        else if (redGuardianKillingBlows > redGolemKillingBlows)
        {
            yellowTeamMVP = PlayerType.GUARDIAN;
        }
        else if (redGolemKillingBlows == redGuardianKillingBlows)
        {
            yellowTeamMVP = PlayerType.NONE;
        }

        if (blueGolemKillingBlows > blueGuardianKillingBlows)
        {
            blueTeamMVP = PlayerType.GOLEM;
        }
        else if (blueGuardianKillingBlows > blueGolemKillingBlows)
        {
            blueTeamMVP = PlayerType.GUARDIAN;
        }
        else if (blueGolemKillingBlows == blueGuardianKillingBlows)
        {
            blueTeamMVP = PlayerType.NONE;
        }

        UI.PassInformationToGameOverUI(winningTeam, yellowTeamMVP, blueTeamMVP);
    }
}
