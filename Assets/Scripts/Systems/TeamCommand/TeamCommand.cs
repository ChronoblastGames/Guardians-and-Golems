using System.Collections;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    private GlobalVariables globalVariables;

    [Header("Command Attributes")]
    public float minCommand;
    public float maxCommand;
    public float startingCommand;

    [Header("Team Command Values")]
    public float redTeamCurrentCommand;
    public float blueTeamCurrentCommand;

    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        globalVariables = GameObject.FindGameObjectWithTag("GlobalVariables").GetComponent<GlobalVariables>();

        redTeamCurrentCommand = startingCommand;
        blueTeamCurrentCommand = startingCommand;
    }

    private void Update()
    {
        ManageCommand();
    }

    void ManageCommand()
    {
    
    }

    void Lose(PlayerTeam teamColor)
    {
        Debug.Log(teamColor + " is out of Command!");
    }

    public void LoseCommand(float commandValue, PlayerTeam teamColor)
    {
        if (teamColor == PlayerTeam.RED)
        {

        }
        else if (teamColor == PlayerTeam.BLUE)
        {

        }
    }

    public void GainCommand(float commandValue, PlayerTeam teamColor)
    {
        if (teamColor == PlayerTeam.RED)
        {

        }
        else if (teamColor == PlayerTeam.BLUE)
        {

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

    public void KillGolem(PlayerTeam teamColor)
    {
        if (teamColor == PlayerTeam.RED)
        {
            GainCommand(globalVariables.golemKillCommandCost, teamColor);
        }
        else if (teamColor == PlayerTeam.BLUE)
        {
            GainCommand(globalVariables.golemKillCommandCost, teamColor);
        }
    }

    public void GolemDeath(PlayerTeam teamColor)
    {
        if (teamColor == PlayerTeam.RED)
        {
            LoseCommand(globalVariables.golemDeathCommandCost, teamColor);

        }
        else if (teamColor == PlayerTeam.BLUE)
        {
            LoseCommand(globalVariables.golemDeathCommandCost, teamColor);
        }
    }
}
