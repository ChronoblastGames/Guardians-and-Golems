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
    }

    void Lose()
    {

    }
}
