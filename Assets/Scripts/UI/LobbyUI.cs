using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour 
{
    [Header("Players")]
    public LobbyPlayerManager playerOne;
    public LobbyPlayerManager playerTwo;
    public LobbyPlayerManager playerThree;
    public LobbyPlayerManager playerFour;

    [Header("UI Attributes")]
    public Text playerOneText;
    public Text playerTwoText;
    public Text playerThreeText;
    public Text playerFourText;
    [Space(10)]
    public Image playerOneReadyImage;
    public Image playerTwoReadyImage;
    public Image playerThreeReadyImage;
    public Image playerFourReadyImage;

    private void Update()
    {
        ManagePlayerText();
        ManagePlayerReady();
    }

    private void ManagePlayerText()
    {
        if (playerOne.currentPlayerType == PlayerType.GOLEM)
        {
            playerOneText.text = "P1 - Golem";
        }
        else if (playerOne.currentPlayerType == PlayerType.GUARDIAN)
        {
            playerOneText.text = "P1 - Guardian";
        }
        else if (playerOne.currentPlayerType == PlayerType.NONE)
        {
            playerOneText.text = "P1 - ";
        }

        if (playerTwo.currentPlayerType == PlayerType.GOLEM)
        {
            playerTwoText.text = "P2 - Golem";
        }
        else if (playerTwo.currentPlayerType == PlayerType.GUARDIAN)
        {
            playerTwoText.text = "P2 - Guardian";
        }
        else if (playerTwo.currentPlayerType == PlayerType.NONE)
        {
            playerTwoText.text = "P2 - ";
        }

        if (playerThree.currentPlayerType == PlayerType.GOLEM)
        {
            playerThreeText.text = "P3 - Golem";
        }
        else if (playerThree.currentPlayerType == PlayerType.GUARDIAN)
        {
            playerThreeText.text = "P3 - Guardian";
        }
        else if (playerThree.currentPlayerType == PlayerType.NONE)
        {
            playerThreeText.text = "P3 - ";
        }

        if (playerFour.currentPlayerType == PlayerType.GOLEM)
        {
            playerFourText.text = "P4 - Golem";
        }
        else if (playerFour.currentPlayerType == PlayerType.GUARDIAN)
        {
            playerFourText.text = "P4 - Guardian";
        }
        else if (playerFour.currentPlayerType == PlayerType.NONE)
        {
            playerFourText.text = "P4 - ";
        }
    }

    private void ManagePlayerReady()
    {
        if (playerOne.isReady)
        {
            playerOneReadyImage.color = Colors.Green;
        }
        else
        {
            playerOneReadyImage.color = Colors.Red;
        }

        if (playerTwo.isReady)
        {
            playerTwoReadyImage.color = Colors.Green;
        }
        else
        {
            playerTwoReadyImage.color = Colors.Red;
        }

        if (playerThree.isReady)
        {
            playerThreeReadyImage.color = Colors.Green;
        }
        else
        {
            playerThreeReadyImage.color = Colors.Red;
        }

        if (playerFour.isReady)
        {
            playerFourReadyImage.color = Colors.Green;
        }
        else
        {
            playerFourReadyImage.color = Colors.Red;
        }
    }
}
