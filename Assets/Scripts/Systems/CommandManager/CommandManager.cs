using System.Collections;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    private StatTracker statTracker;

    private UIManager UI;

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

    [Header("Command Text Locations")]
    public Transform yellowTeamTextLocation;
    public Transform blueTeamTextLocation;

    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        globalVariables = GameObject.FindGameObjectWithTag("GlobalVariables").GetComponent<GlobalVariables>();

        statTracker = GameObject.FindGameObjectWithTag("StatTracker").GetComponent<StatTracker>();

        UI = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

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
            if (redGuardianPlayerController.conduitCapturedList.Count <= 1)
            {
                statTracker.InformationPassthrough(PlayerTeam.RED);
                Time.timeScale = 0;
            }
        }

        if (isBlueTeamLosing)
        {
            if (blueGuardianPlayerController.conduitCapturedList.Count <= 1)
            {
                statTracker.InformationPassthrough(PlayerTeam.BLUE);
                Time.timeScale = 0;
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
                if (!isRedTeamLosing)
                {
                    foreach (GameObject conduit in redGuardianPlayerController.conduitCapturedList)
                    {
                        if (conduit.GetComponent<ConduitController>().conduitState != ConduitState.HOMEBASE)
                        {
                            conduit.GetComponent<ConduitController>().conduitState = ConduitState.LOSING;
                        }
                    }

                    isRedTeamLosing = true;

                    UI.RequestGenericText("Yellow Team on the verge of Losing!", yellowTeamTextLocation, Colors.YellowTeamColor);
                }          
                break;

            case PlayerTeam.BLUE:
                if (!isBlueTeamLosing)
                {
                    foreach (GameObject conduit in blueGuardianPlayerController.conduitCapturedList)
                    {
                        conduit.GetComponent<ConduitController>().conduitState = ConduitState.LOSING;
                    }

                    isBlueTeamLosing = true;

                    UI.RequestGenericText("Blue Team on the verge of Losing!", blueTeamTextLocation, Colors.BlueTeamColor);
                }            
                break;
        }
    }

    public void GainCommand(float commandValue, PlayerTeam teamColor)
    {
        if (teamColor == PlayerTeam.RED)
        {
            redTeamCurrentCommand += commandValue;

            UI.RequestGenericText("Gained " + commandValue + " command!", yellowTeamTextLocation, Colors.YellowTeamColor);

            if (isRedTeamLosing)
            {
                isRedTeamLosing = false;
            }
        }
        else if (teamColor == PlayerTeam.BLUE)
        {
            blueTeamCurrentCommand += commandValue;

            UI.RequestGenericText("Gained " + commandValue + " command!", blueTeamTextLocation, Colors.BlueTeamColor);

            if (isBlueTeamLosing)
            {
                isBlueTeamLosing = false;
            }
        }
    }

    public void LoseCommand(float commandValue, PlayerTeam teamColor)
    {
        if (teamColor == PlayerTeam.RED)
        {
            redTeamCurrentCommand -= commandValue;

            UI.RequestGenericText("Lost " + commandValue + " command!", yellowTeamTextLocation, Colors.YellowTeamColor);
        }
        else if (teamColor == PlayerTeam.BLUE)
        {
            blueTeamCurrentCommand -= commandValue;

            UI.RequestGenericText("Lost " + commandValue + " command!", blueTeamTextLocation, Colors.BlueTeamColor);
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
