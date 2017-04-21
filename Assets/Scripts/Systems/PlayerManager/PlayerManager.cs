using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PlayerInfoManager playerInfoManager;

    [Header("Player Attributes")]
    private PlayerInfo playerOne;
    private PlayerInfo playerTwo;
    private PlayerInfo playerThree;
    private PlayerInfo playerFour;

    private List<PlayerInfo> playerInfoList;

    public bool isDevMode = false;

    private void Start()
    {
        playerInfoManager = GameObject.FindGameObjectWithTag("PlayerInfoManager").GetComponent<PlayerInfoManager>();

        SetupPlayers();
    }

    private void SetupPlayers()
    {
        if (!isDevMode)
        {
            playerInfoList = playerInfoManager.playerInfoList;

            foreach (PlayerInfo playerInfo in playerInfoList)
            {
                switch (playerInfo.playerColor)
                {
                    case PlayerTeam.RED:
                        if (playerInfo.playerType == PlayerType.GOLEM)
                        {
                            GameObject newGolem = GameObject.FindGameObjectWithTag("GolemRed");
                            newGolem.GetComponent<GolemInputController>().playerNum = playerInfo.playerNumber;
                            newGolem.GetComponent<GolemInputController>().PlayerSetup();
                        }
                        else if (playerInfo.playerType == PlayerType.GUARDIAN)
                        {
                            GameObject newGuardian = GameObject.FindGameObjectWithTag("GuardianRed");
                            newGuardian.GetComponent<GuardianInputController>().playerNum = playerInfo.playerNumber;
                            newGuardian.GetComponent<GuardianInputController>().PlayerSetup();
                        }
                        break;

                    case PlayerTeam.BLUE:
                        if (playerInfo.playerType == PlayerType.GOLEM)
                        {
                            GameObject newGolem = GameObject.FindGameObjectWithTag("GolemBlue");
                            newGolem.GetComponent<GolemInputController>().playerNum = playerInfo.playerNumber;
                            newGolem.GetComponent<GolemInputController>().PlayerSetup();
                        }
                        else if (playerInfo.playerType == PlayerType.GUARDIAN)
                        {
                            GameObject newGuardian = GameObject.FindGameObjectWithTag("GuardianBlue");
                            newGuardian.GetComponent<GuardianInputController>().playerNum = playerInfo.playerNumber;
                            newGuardian.GetComponent<GuardianInputController>().PlayerSetup();
                        }
                        break;
                }
            }
        }   
    }
}
