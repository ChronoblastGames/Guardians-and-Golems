using System.Collections;
using UnityEngine;
using Rewired;

public class GuardianInputController : MonoBehaviour 
{
    private Player playerInput;

    private GuardianSpellIndicatorController guardianSpellIndicator;

    private GuardianPlayerController guardianPlayerController;

    [Header("Guardian Player Values")]
    public PlayerNum playerNum;
    public PlayerTeam playerTeam;

    public string playerName;
    public int playerNumber;

    [Header("Guardian Input Values")]
    public Vector3 moveVec;
    [Space(10)]
    public Vector3 aimVec;

    [HideInInspector]
    public float xAxis;
    [HideInInspector]
    public float zAxis;

    public float holdTime;
    public float holdTimeMultiplier = 2;
    private float maxHoldTime = 10f;

    [HideInInspector]
    public float aimXAxis;
    [HideInInspector]
    public float aimZAxis;

    private bool isInitialized = false;

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
        guardianSpellIndicator = GetComponent<GuardianSpellIndicatorController>();

        guardianPlayerController = GetComponent<GuardianPlayerController>();
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

        isInitialized = true;
    }

    private void GetInput()
    {
        if (isInitialized)
        {
            xAxis = playerInput.GetAxis("HorizontalMovement");
            zAxis = playerInput.GetAxis("VerticalMovement");

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

            if (playerInput.GetButton("Melee/Capture"))
            {
                guardianPlayerController.CaptureCurrentConduit();
            }
            else if (playerInput.GetButtonUp("Melee/Capture"))
            {
                guardianPlayerController.StopCapturingConduit();
            }

            if (playerInput.GetButton("Ability1"))
            {
                guardianSpellIndicator.SetNewIndicator(guardianPlayerController.guardianAbilites[0].indicatorType, guardianPlayerController.guardianAbilites[0].indicatorSize);

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
                guardianPlayerController.UseAbility(0, playerTeam, holdTime);
                holdTime = 0;

                guardianSpellIndicator.SetNewIndicator(IndicatorType.ARROW, 0);
            }

            if (playerInput.GetButton("Ability2"))
            {
                guardianSpellIndicator.SetNewIndicator(guardianPlayerController.guardianAbilites[1].indicatorType, guardianPlayerController.guardianAbilites[1].indicatorSize);
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
                guardianPlayerController.UseAbility(1, playerTeam, holdTime);
                holdTime = 0;

                guardianSpellIndicator.SetNewIndicator(IndicatorType.ARROW, 0);
            }

            if (playerInput.GetButton("Ability3"))
            {
                guardianSpellIndicator.SetNewIndicator(guardianPlayerController.guardianAbilites[2].indicatorType, guardianPlayerController.guardianAbilites[2].indicatorSize);
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
                guardianPlayerController.UseAbility(2, playerTeam, holdTime);
                holdTime = 0;

                guardianSpellIndicator.SetNewIndicator(IndicatorType.ARROW, 0);
            }

            if (playerInput.GetButton("Ability4"))
            {
                guardianSpellIndicator.SetNewIndicator(guardianPlayerController.guardianAbilites[3].indicatorType, guardianPlayerController.guardianAbilites[3].indicatorSize);
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
                guardianPlayerController.UseAbility(3, playerTeam, holdTime);
                holdTime = 0;

                guardianSpellIndicator.SetNewIndicator(IndicatorType.ARROW, 0);
            }
        }
    }        
}
