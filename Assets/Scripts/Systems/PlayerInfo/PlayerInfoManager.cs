using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoManager : MonoBehaviour
{
    [Header("Player Info")]
    public List<PlayerInfo> playerInfoList = new List<PlayerInfo>();

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}

public struct PlayerInfo
{
    public PlayerNum playerNumber;
    public PlayerTeam playerColor;
    public PlayerType playerType;
}
