using System.Collections;
using UnityEngine;
using Rewired;

public class LobbyPlayerManager : MonoBehaviour 
{
    private LobbyManager lobbyManager;

    private Player playerInput;

    [Header("Player Attributes")]
    public PlayerNum playerNum;
    public int playerNumber;

    public bool isReady = false;

    private GameObject playerIcon;

    public float selectionCooldownDelay = 0.25f;

    [Header("Input Attributes")]
    private bool canMove = true;

    [Header("Team Attributes")]
    public PlayerTeam currentTeamColor;

    private bool isInLobby = false;
    private bool isOnTeam = false;
    private bool isOnRedTeam = false;
    private bool isOnBlueTeam = false;
    private bool isGolem = false;
    private bool isGuardian = false;

    private void Start()
    {
        Initialize();
    }
    
    private void Update()
    {
        GetInput();
    }
    
    private void Initialize()
    {
        playerIcon = transform.GetChild(0).gameObject;

        lobbyManager = GameObject.FindGameObjectWithTag("LobbyManager").GetComponent<LobbyManager>();

        switch (playerNum)
        {
            case PlayerNum.PLAYER_1:
                playerNumber = 0;
                break;

            case PlayerNum.PLAYER_2:
                playerNumber = 1;
                break;

            case PlayerNum.PLAYER_3:
                playerNumber = 2;
                break;

            case PlayerNum.PLAYER_4:
                playerNumber = 3;
                break;
        }

        playerInput = ReInput.players.GetPlayer(playerNumber);
    }

    private void GetInput()
    {
        if (playerInput.GetButton("MoveRight"))
        {
            if (canMove)
            {
                if (isInLobby)
                {
                    Move(Direction.RIGHT);
                    StartCoroutine(SelectionCooldown(selectionCooldownDelay));
                }
            }
        }

        if (playerInput.GetButton("MoveLeft"))
        {
            if (canMove)
            {
                if (isInLobby)
                {
                    Move(Direction.LEFT);
                    StartCoroutine(SelectionCooldown(selectionCooldownDelay));
                }
            }         
        }

        if (playerInput.GetButton("MoveUp"))
        {
            if (canMove)
            {
                if (isInLobby)
                {
                    Move(Direction.UP);
                    StartCoroutine(SelectionCooldown(selectionCooldownDelay));
                }
            }      
        }

        if (playerInput.GetButton("MoveDown"))
        {
            if (canMove)
            {
                if (isInLobby)
                {
                    Move(Direction.DOWN);
                    StartCoroutine(SelectionCooldown(selectionCooldownDelay));
                }
            }          
        }

        if (playerInput.GetButtonDown("Ready"))
        {
            if (isInLobby)
            {
                if (isOnTeam)
                {
                    if (isGolem)
                    {
                        isReady = true;
                    }
                    else if (isGuardian)
                    {
                        isReady = true;
                    }
                }
            }
            else
            {
                lobbyManager.JoinLobby(gameObject);
                playerIcon.SetActive(true);
                isInLobby = true;
            }         
        }

        if (playerInput.GetButtonDown("Unready"))
        {
            if (isInLobby)
            {
                if (isOnTeam)
                {
                    if (isReady)
                    {
                        isReady = false;
                    }
                }
            }     
        }
    }

    private void Move(Direction moveDirection)
    {
        switch (moveDirection)
        {
            case Direction.LEFT:
                if (!isReady)
                {
                    if (!isOnTeam)
                    {
                        isOnTeam = true;
                        isOnRedTeam = true;
                        lobbyManager.redTeamPlayers.Add(gameObject);
                        lobbyManager.unselectedPlayers.Remove(gameObject);
                        transform.position = lobbyManager.redTeamLocations[playerNumber].position;
                    }
                    else if (isOnBlueTeam)
                    {
                        isOnTeam = false;
                        isOnBlueTeam = false;
                        lobbyManager.blueTeamPlayers.Remove(gameObject);
                        lobbyManager.unselectedPlayers.Add(gameObject);
                        transform.position = lobbyManager.noTeamLocations[playerNumber].position;
                    }
                }       
                break;

            case Direction.RIGHT:
                if (!isReady)
                {
                    if (!isOnTeam)
                    {
                        isOnTeam = true;
                        isOnBlueTeam = true;
                        lobbyManager.blueTeamPlayers.Add(gameObject);
                        lobbyManager.unselectedPlayers.Remove(gameObject);
                        transform.position = lobbyManager.blueTeamLocations[playerNumber].position;
                    }
                    else if (isOnRedTeam)
                    {
                        isOnTeam = false;
                        isOnRedTeam = false;
                        lobbyManager.redTeamPlayers.Remove(gameObject);
                        lobbyManager.unselectedPlayers.Add(gameObject);
                        transform.position = lobbyManager.noTeamLocations[playerNumber].position;
                    }                   
                }            
                break;

            case Direction.UP:
                if (!isReady)
                {
                    if (isOnTeam)
                    {
                        if (!isGuardian)
                        {
                            if (isOnRedTeam)
                            {
                                if (lobbyManager.redTeamGolem == null)
                                {
                                    lobbyManager.AddGolem(PlayerTeam.RED, gameObject);
                                    isGolem = true;
                                    transform.position = lobbyManager.redTeamGolemPosition.position;
                                }
                            }
                            else if (isOnBlueTeam)
                            {
                                if (lobbyManager.blueTeamGolem == null)
                                {
                                    lobbyManager.AddGolem(PlayerTeam.BLUE, gameObject);
                                    isGolem = true;
                                    transform.position = lobbyManager.blueTeamGolemPosition.position;
                                }
                            }
                        }
                        else if (isGuardian)
                        {
                            if (isOnRedTeam)
                            {
                                lobbyManager.RemoveGuardian(PlayerTeam.RED);
                                isGuardian = false;
                                transform.position = lobbyManager.redTeamLocations[playerNumber].position;
                            }
                            else if (isOnBlueTeam)
                            {
                                lobbyManager.RemoveGuardian(PlayerTeam.BLUE);
                                isGuardian = false;
                                transform.position = lobbyManager.blueTeamLocations[playerNumber].position;
                            }
                        }
                    }
                }          
                break;

            case Direction.DOWN:
                if (!isReady)
                {
                    if (isOnTeam)
                    {
                        if (!isGolem)
                        {
                            if (isOnRedTeam)
                            {
                                if (lobbyManager.redTeamGuardian == null)
                                {
                                    lobbyManager.AddGuardian(PlayerTeam.RED, gameObject);
                                    isGuardian = true;
                                    transform.position = lobbyManager.redTeamGuardianPosition.position;
                                }
                            }
                            else if (isOnBlueTeam)
                            {
                                if (lobbyManager.blueTeamGuardian == null)
                                {
                                    lobbyManager.AddGuardian(PlayerTeam.BLUE, gameObject);
                                    isGuardian = true;
                                    transform.position = lobbyManager.blueTeamGuardianPositon.position;
                                }
                            }
                        }
                        else if (isGolem)
                        {
                            if (isOnRedTeam)
                            {
                                lobbyManager.RemoveGolem(PlayerTeam.RED);
                                isGolem = false;
                                transform.position = lobbyManager.redTeamLocations[playerNumber].position;
                            }
                            else if (isOnBlueTeam)
                            {
                                lobbyManager.RemoveGolem(PlayerTeam.BLUE);
                                isGolem = false;
                                transform.position = lobbyManager.blueTeamLocations[playerNumber].position;
                            }
                        }
                    }
                }             
                break;
        }
    }

    private IEnumerator SelectionCooldown(float cooldownTime)
    {
        canMove = false;

        yield return new WaitForSeconds(cooldownTime);

        canMove = true;

        yield return null;
    }
}
