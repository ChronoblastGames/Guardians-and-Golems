using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour 
{
    [Header("Game Over Attributes")]
    public GameObject UIHolder;

    public Text winningTeamText;
    [Space(10)]
    public Text yellowTeamMVPText;
    public Text blueTeamMVPText;

    private void Start()
    {
    
    }
    
    private void Update()
    {
    
    }
    
    public void PassInfoThrough(PlayerTeam winningTeam, PlayerType yellowTeamMVP, PlayerType blueTeamMVP)
    {
        if (winningTeam == PlayerTeam.RED)
        {
            winningTeamText.text = "Yellow Wins!";
            winningTeamText.color = Colors.YellowTeamColor;
        }
        else if (winningTeam == PlayerTeam.BLUE)
        {
            winningTeamText.text = "Blue Wins!";
            winningTeamText.color = Colors.BlueTeamColor;
        }

        switch (yellowTeamMVP)
        {
            case PlayerType.GOLEM:
                yellowTeamMVPText.text = "Yellow MVP: GOLEM";
                break;

            case PlayerType.GUARDIAN:
                yellowTeamMVPText.text = "Yellow MVP: GUARDIAN";
                break;

            case PlayerType.NONE:
                yellowTeamMVPText.text = "Yellow MVP: Both Players! You Rock!";
                break;
        }

        switch (blueTeamMVP)
        {
            case PlayerType.GOLEM:
                blueTeamMVPText.text = "Blue MVP: GOLEM";
                break;

            case PlayerType.GUARDIAN:
                blueTeamMVPText.text = "Blue MVP: GUARDIAN";
                break;

            case PlayerType.NONE:
                blueTeamMVPText.text = "Blue MVP: Both Players! You Rock!";
                break;
        }

        UIHolder.SetActive(true);
    }
}
