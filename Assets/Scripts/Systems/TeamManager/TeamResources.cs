using System.Collections;
using UnityEngine;

public class TeamResources : MonoBehaviour
{
    [Header("Team Resources")]
    public PlayerTeam teamColor;

    [Header("Crystal Attributes")]
    public int minCrystals;
    public int currentCrystal;
    public int maxCrystals;

    public float crystalRegenerationRate;

    public bool canRegenerateCrystals = false;
    public bool canUseCrystals = true;

    [Header("Command Attributes")]
    public float currentCommand;
    public float minCommand;
    public float maxCommand;

    private void Update()
    {
        ManageCommand();

        ManageCrystals();
    }

    void ManageCommand()
    {
        if (currentCommand < minCommand)
        {
            currentCommand = minCommand;
        }
    }

    void ManageCrystals()
    {

    }

    public bool UseCrystal(int crystalAmount)
    {
        if (currentCrystal >= crystalAmount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
