using System.Collections;
using UnityEngine;

public class TeamResources : MonoBehaviour
{
    [Header("Team Resources")]
    public PlayerTeam teamColor;

    [Header("Command Attributes")]
    public float currentCommand;
    public float minCommand;
    public float maxCommand;
    public float startingCommand;

    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        currentCommand = startingCommand;
    }

    private void Update()
    {
        ManageCommand();
    }

    void ManageCommand()
    {
        if (currentCommand <= 0)
        {
            Lose();
        }

        if (currentCommand > maxCommand)
        {
            currentCommand = maxCommand;
        }
    }

    void Lose()
    {
        Debug.Log(teamColor + " is out of Command!");
    }

    public void LoseCommand(float commandValue)
    {
        currentCommand -= commandValue;

        if (currentCommand <= 0)
        {
            Lose();
        }
    }

    public void GainCommand(float commandValue)
    {
        currentCommand += commandValue;

        if (currentCommand > maxCommand)
        {
            currentCommand = maxCommand;
        }
    }
}
