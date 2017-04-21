using System.Collections;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    private GuardianPlayerController redGuardianPlayerController;
    private GuardianPlayerController blueGuardianPlayerController;

    private GlobalVariables globalVariables;

    [Header("Command Attributes")]
    public float minCommand;
    public float maxCommand;
    public float startingCommand;

    [Header("Team Command Values")]
    public float redTeamCurrentCommand;
    public float blueTeamCurrentCommand;

    public bool isRedTeamLosing = false;
    public bool isBlueTeamLosing = false;

    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        globalVariables = GameObject.FindGameObjectWithTag("GlobalVariables").GetComponent<GlobalVariables>();

        redGuardianPlayerController = GameObject.FindGameObjectWithTag("GuardianRed").GetComponent<GuardianPlayerController>();
        blueGuardianPlayerController = GameObject.FindGameObjectWithTag("GuardianBlue").GetComponent<GuardianPlayerController>();

        redTeamCurrentCommand = startingCommand;
        blueTeamCurrentCommand = startingCommand;
    }

    private void Update()
    {
        ManageCommand();
    }

    void ManageCommand()
    {
        if (isRedTeamLosing)
        { 
            if (redGuardianPlayerController.conduitCapturedList.Count == 1)
            {
                Debug.Log("Red Team Lost!");
            }
        }

        if (isBlueTeamLosing)
        {
            if (blueGuardianPlayerController.conduitCapturedList.Count == 1)
            {
                Debug.Log("Blue Team Lost");
            }
        }

        if (!isRedTeamLosing)
        {
            if (redTeamCurrentCommand <= 0)
            {
                Lose(PlayerTeam.RED);
            }
        }

        if (!isBlueTeamLosing)
        {
            if (blueTeamCurrentCommand <= 0)
            {
                Lose(PlayerTeam.BLUE);
            }
        }
    }

    void Lose(PlayerTeam teamColor)
    {
        switch (teamColor)
        {
            case PlayerTeam.RED:
                foreach(GameObject conduit in redGuardianPlayerController.conduitCapturedList)
                {
                    conduit.GetComponent<ConduitController>().conduitState = ConduitState.DRAINING;
                }

                isRedTeamLosing = true;
                break;

            case PlayerTeam.BLUE:
                foreach (GameObject conduit in blueGuardianPlayerController.conduitCapturedList)
                {
                    conduit.GetComponent<ConduitController>().conduitState = ConduitState.DRAINING;
                }

                isBlueTeamLosing = true;
                break;
        }
    }

    public void GainCommand(float commandValue, PlayerTeam teamColor)
    {
        if (teamColor == PlayerTeam.RED)
        {
            redTeamCurrentCommand += commandValue;
        }
        else if (teamColor == PlayerTeam.BLUE)
        {
            blueTeamCurrentCommand += commandValue;
        }
    }

    public void LoseCommand(float commandValue, PlayerTeam teamColor)
    {
        if (teamColor == PlayerTeam.RED)
        {
            redTeamCurrentCommand -= commandValue;
        }
        else if (teamColor == PlayerTeam.BLUE)
        {
            blueTeamCurrentCommand -= commandValue;
        }
    }

    public void ConduitCapture(PlayerTeam teamColor)
    {
        if (teamColor == PlayerTeam.RED)
        {
            GainCommand(globalVariables.winConduitCommandCost, teamColor);
        }
        else if (teamColor == PlayerTeam.BLUE)
        {
            GainCommand(globalVariables.winConduitCommandCost, teamColor);
        }
    }

    public void LoseConduit(PlayerTeam teamColor)
    {
        if (teamColor == PlayerTeam.RED)
        {
            LoseCommand(globalVariables.loseConduitCommandCost, teamColor);
        }
        else if (teamColor == PlayerTeam.BLUE)
        {
            LoseCommand(globalVariables.loseConduitCommandCost, teamColor);
        }
    }

    public void GolemDeath(PlayerTeam teamColor)
    {
        if (teamColor == PlayerTeam.RED)
        {
            LoseCommand(globalVariables.golemDeathCommandCost, teamColor);
            GainCommand(globalVariables.golemKillCommandCost, PlayerTeam.BLUE);
        }
        else if (teamColor == PlayerTeam.BLUE)
        {
            LoseCommand(globalVariables.golemDeathCommandCost, teamColor);
            GainCommand(globalVariables.golemKillCommandCost, PlayerTeam.RED);
        }
    }
}
