using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoManager : MonoBehaviour
{
    [Header("Player Info")]
    public PlayerInfo playerOneInfo;
    public PlayerInfo playerTwoInfo;
    public PlayerInfo playerThreeInfo;
    public PlayerInfo playerFourInfo;

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
