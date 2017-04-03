using System.Collections;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    [Header("Team Resources")]
    public float currentCommand;
    public float minCommand;
    public float maxCommand;

    [Space(10)]
    public int nodeCount;

    private void Update()
    {
        ManageCommand();
    }

    void ManageCommand()
    {
        if (currentCommand < minCommand)
        {
            currentCommand = minCommand;
        }
    }
}
