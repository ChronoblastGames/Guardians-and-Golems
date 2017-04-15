using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    [Header("Lobby Attributes")]
    public List<GameObject> totalPlayers;
    [Space(10)]
    public List<GameObject> unselectedPlayers;
    [Space(10)]
    public List<GameObject> redTeamPlayers;
    [Space(10)]
    public List<GameObject> blueTeamPlayers;

    private bool isRedTeamReady = false;
    private bool isBlueTeamReady = false;

    public void JoinGame(int playerNumber)
    {

    }

    public void JoinTeam(PlayerTeam teamColor, int playerNumber)
    {
        switch (teamColor)
        {
            case PlayerTeam.RED:
                break;

            case PlayerTeam.BLUE:
                break;
        }
    }

    public void LeaveTeam(int playerNumber)
    {
       
    }

    public void StartGame()
    {

    }

    private bool IsTeamReady(PlayerTeam teamColor)
    {
        switch(teamColor)
        {
            case PlayerTeam.RED:
                if (redTeamPlayers.Count == 2)
                {
                    return true;
                }
                break;

            case PlayerTeam.BLUE:
                if (blueTeamPlayers.Count == 2)
                {
                    return true;
                }
                break;
        }

        return false;
    }
}
