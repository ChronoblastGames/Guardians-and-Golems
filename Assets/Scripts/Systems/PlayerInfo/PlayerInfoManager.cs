using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoManager : MonoBehaviour
{
    private static PlayerInfoManager playerInfoManagerInstance;

    [Header("Player Info")]
    public List<PlayerInfo> playerInfoList = new List<PlayerInfo>();

    private void Start()
    {
        if (playerInfoManagerInstance != null && playerInfoManagerInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            playerInfoManagerInstance = this;
        }

        DontDestroyOnLoad(gameObject);
    }
}

public struct PlayerInfo
{
    public PlayerNum playerNumber;
    public PlayerTeam playerColor;
    public PlayerType playerType;
}
