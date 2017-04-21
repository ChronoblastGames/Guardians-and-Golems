using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    private PlayerInfoManager playerInfoManager;

    [Header("Lobby Attributes")]
    public List<GameObject> totalPlayers;
    [Space(10)]
    public List<GameObject> unselectedPlayers;
    [Space(10)]
    public List<GameObject> redTeamPlayers;
    [Space(10)]
    public List<GameObject> blueTeamPlayers;
    [Space(10)]
    public GameObject redTeamGolem;
    public GameObject redTeamGuardian;
    [Space(10)]
    public GameObject blueTeamGolem;
    public GameObject blueTeamGuardian;

    private bool isRedTeamReady = false;
    private bool isBlueTeamReady = false;

    [Header("Locations")]
    public List<Transform> noTeamLocations;
    public List<Transform> redTeamLocations;
    public List<Transform> blueTeamLocations;
    [Space(10)]
    public Transform redTeamGolemPosition;
    public Transform redTeamGuardianPosition;
    [Space(10)]
    public Transform blueTeamGolemPosition;
    public Transform blueTeamGuardianPositon;

    private void Start()
    {
        playerInfoManager = GameObject.FindGameObjectWithTag("PlayerInfoManager").GetComponent<PlayerInfoManager>();
    }

    public void JoinLobby(GameObject playerObject)
    {
        unselectedPlayers.Add(playerObject);
    }

    public void JoinTeam(PlayerTeam teamColor, int playerNumber, GameObject playerObject)
    {
        switch (teamColor)
        {
            case PlayerTeam.RED:
                break;

            case PlayerTeam.BLUE:
                break;
        }
    }

    public void LeaveTeam(PlayerTeam teamColor, int playerNumber, GameObject playerObject)
    {
        switch (teamColor)
        {
            case PlayerTeam.RED:
                
                break;

            case PlayerTeam.BLUE:
                break;
        }
    }

    public bool CheckForPlayer(PlayerTeam teamColor, PlayerType playerType)
    {
        switch (teamColor)
        {
            case PlayerTeam.RED:
                if (playerType == PlayerType.GOLEM)
                {

                }
                else if (playerType == PlayerType.GUARDIAN)
                {

                }
                break;

            case PlayerTeam.BLUE:
                if (playerType == PlayerType.GOLEM)
                {

                }
                else if (playerType == PlayerType.GUARDIAN)
                {

                }
                break;
        }

        return false;
    }

    private bool CheckForReady()
    {
        foreach(GameObject player in totalPlayers)
        {
            if (player.GetComponent<LobbyPlayerManager>().isReady)
            {
                continue;
            }
            else
            {
                return false;
            }
        }

        return true;
    }

    public void AddGolem(PlayerTeam teamColor, GameObject playerObject)
    {
        switch (teamColor)
        {
            case PlayerTeam.RED:
                redTeamGolem = playerObject;
                break;

            case PlayerTeam.BLUE:
                blueTeamGolem = playerObject;
                break;
        }
    }

    public void RemoveGolem(PlayerTeam teamColor)
    {
        switch (teamColor)
        {
            case PlayerTeam.RED:
                redTeamGolem = null;
                break;

            case PlayerTeam.BLUE:
                blueTeamGolem = null;
                break;
        }
    }

    public void AddGuardian(PlayerTeam teamColor, GameObject playerObject)
    {
        switch (teamColor)
        {
            case PlayerTeam.RED:
                redTeamGuardian = playerObject;
                break;

            case PlayerTeam.BLUE:
                blueTeamGuardian = playerObject;
                break;
        }
    }

    public void RemoveGuardian(PlayerTeam teamColor)
    {
        switch (teamColor)
        {
            case PlayerTeam.RED:
                redTeamGuardian = null;
                break;

            case PlayerTeam.BLUE:
                blueTeamGuardian = null;
                break;
        }
    }


    public void StartGame()
    {
        if (CheckForReady())
        {
            PassInformationToPlayerInfo();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);

        }
    }

    private void PassInformationToPlayerInfo()
    {
        foreach (GameObject player in totalPlayers)
        {
            PlayerInfo newPlayerInfo = player.GetComponent<LobbyPlayerManager>().ReturnPlayerInfo();

            playerInfoManager.playerInfoList.Add(newPlayerInfo);
        }
    }
}
