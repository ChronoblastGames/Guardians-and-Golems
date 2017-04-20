using System.Collections;
using UnityEngine;
using Rewired;

public class GolemInputController : MonoBehaviour 
{
    private GolemSpellIndicatorController golemSpellIndicator;

    private GolemPlayerController golemPlayerController;

    private Player playerInput;

    [Header("Player Input Attributes")]
    public PlayerNum playerNum;
    [Space(10)]
    public PlayerTeam playerTeam;

    public int playerNumber;
    [Space(10)]
    public string playerName;

    [HideInInspector]
    public float xAxis;
    [HideInInspector]
    public float zAxis;
    [HideInInspector]
    public float aimXAxis;
    [HideInInspector]
    public float aimZAxis;

    [Space(10)]
    public Vector3 moveVec;
    [Space(10)]
    public Vector3 moveDirection;
    [Space(10)]
    public Vector3 aimVec;
    [Space(10)]
    public float holdTime;
    public float holdTimeMultiplier = 2f;

    private float maxHoldTime = 10f;

    public bool isInitialize = false;

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
        golemSpellIndicator = GetComponent<GolemSpellIndicatorController>();

        golemPlayerController = GetComponent<GolemPlayerController>();

        PlayerSetup();
    }

    public void PlayerSetup()
    {
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

        isInitialize = true;
    }

    private void GetInput()
    {
        if (isInitialize)
        {
            xAxis = playerInput.GetAxis("HorizontalMovement");
            zAxis = playerInput.GetAxis("VerticalMovement");

            moveVec = new Vector2(xAxis, zAxis);
            moveDirection = moveVec.normalized;

            aimXAxis = playerInput.GetAxis("HorizontalAimAxis");
            aimZAxis = playerInput.GetAxis("VerticalAimAxis");

            if (aimXAxis != 0 || aimZAxis != 0)
            {
                aimVec = new Vector3(aimXAxis, 0, aimZAxis).normalized;
            }
            else
            {
                aimVec = Vector3.zero;
            }

            if (playerInput.GetButtonDown("Melee/Capture"))
            {
                golemPlayerController.UseAttack();
            }

            if (playerInput.GetButtonDown("Dash"))
            {
                golemPlayerController.DodgeSetup();
            }

            if (playerInput.GetButton("Ability1"))
            {
                golemSpellIndicator.SetNewIndicator(golemPlayerController.golemAbilities[0].indicatorType, golemPlayerController.golemAbilities[0].indicatorSize);

                if (holdTime <= maxHoldTime)
                {
                    holdTime += Time.deltaTime * holdTimeMultiplier;
                }
                else
                {
                    holdTime = maxHoldTime;
                }
            }

            if (playerInput.GetButtonUp("Ability1"))
            {
                golemPlayerController.UseAbility(0, playerTeam, holdTime);
                holdTime = 0;

                golemSpellIndicator.SetNewIndicator(IndicatorType.ARROW, 0);
            }

            if (playerInput.GetButton("Ability2"))
            {
                golemSpellIndicator.SetNewIndicator(golemPlayerController.golemAbilities[1].indicatorType, golemPlayerController.golemAbilities[1].indicatorSize);

                if (holdTime <= maxHoldTime)
                {
                    holdTime += Time.deltaTime * holdTimeMultiplier;
                }
                else
                {
                    holdTime = maxHoldTime;
                }
            }

            if (playerInput.GetButtonUp("Ability2"))
            {
                golemPlayerController.UseAbility(1, playerTeam, holdTime);
                holdTime = 0;

                golemSpellIndicator.SetNewIndicator(IndicatorType.ARROW, 0);
            }

            if (playerInput.GetButton("Ability3"))
            {
                golemSpellIndicator.SetNewIndicator(golemPlayerController.golemAbilities[2].indicatorType, golemPlayerController.golemAbilities[2].indicatorSize);

                if (holdTime <= maxHoldTime)
                {
                    holdTime += Time.deltaTime * holdTimeMultiplier;
                }
                else
                {
                    holdTime = maxHoldTime;
                }
            }

            if (playerInput.GetButtonUp("Ability3"))
            {
                golemPlayerController.UseAbility(2, playerTeam, holdTime);
                holdTime = 0;

                golemSpellIndicator.SetNewIndicator(IndicatorType.ARROW, 0);
            }

            if (playerInput.GetButton("Ability4"))
            {
                golemSpellIndicator.SetNewIndicator(golemPlayerController.golemAbilities[3].indicatorType, golemPlayerController.golemAbilities[3].indicatorSize);

                if (holdTime <= maxHoldTime)
                {
                    holdTime += Time.deltaTime * holdTimeMultiplier;
                }
                else
                {
                    holdTime = maxHoldTime;
                }
            }

            if (playerInput.GetButtonUp("Ability4"))
            {
                golemPlayerController.UseAbility(3, playerTeam, holdTime);
                holdTime = 0;

                golemSpellIndicator.SetNewIndicator(IndicatorType.ARROW, 0);
            }
        }
    }       
}
