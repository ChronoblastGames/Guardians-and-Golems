﻿using System.Collections;
using UnityEngine;

[RequireComponent(typeof (GolemInputManager))]
[System.Serializable]
public class GolemPlayerController : GolemStats 
{
    private BasicCooldown globalCooldown;
    private GlobalVariables globalVariables;
    private GolemInputManager golemInputManager;
    private CharacterController characterController;
    private GolemMelee golemMelee;

    private Animator golemState;

    [Header("Timers")]
    private TimerClass idleTimer;
    private TimerClass recoveryTimer;

    [Header("Player Movement Attributes")]
    public float currentSpeed;
    public float speedSmoothTime = 0.1f;
    private float targetSpeed;
    private float speedSmoothVelocity;
    public float characterVelocity;

    public float idleTime;

    public bool isIdle;

    [Header("Player Dodge Attributes")]
    public float dodgeSmoothTime = 0.1f;
    private float currentDodgeSpeed;
    private float targetDodgeSpeed;
    private float dodgeSmoothVelocity;
    private Vector3 dodgeDirectionVec;
    private Vector3 dodgeDestination;

    [HideInInspector]
    public bool isStaggered;

    private Vector2 moveVec;
    private Vector2 directionVec;

    [Header("Player Turning Attributes")]
    public float turnSmoothTime = 0.2f;
    private float turnSmoothVelocity;

    [Header("Player Gravity Attributes")]
    public float gravity = -12f;
    private float velocityY;

    [Header("Debugging Values")]
    public GameObject blockIndicator;

    [Header("CoolDowns")]
	private float globalCooldownTime;

	void Start () 
    {
        PlayerSetup();
	}
	
	void Update () 
    {
        GatherInput();
	}

    void FixedUpdate()
    {
        ManageMovement();
        ManageDodge();
        ManageIdle();
        ManageRecoveryTime();
    }

    void PlayerSetup()
    {
		globalCooldown = new BasicCooldown();

        globalVariables = GameObject.FindObjectOfType<GlobalVariables>();

        globalCooldownTime = globalVariables.golemGlobalCooldown;

		globalCooldown.cdTime = globalCooldownTime;

        golemInputManager = GetComponent<GolemInputManager>();

        golemMelee = GetComponent<GolemMelee>();

        characterController = GetComponent<CharacterController>();

        idleTimer = new TimerClass();

        recoveryTimer = new TimerClass();

        golemState = GetComponent<Animator>();

        idleTimer.ResetTimer(idleTime);
    }

    void GatherInput()
    {
        moveVec = golemInputManager.moveVec;
        directionVec = golemInputManager.moveDirection;
    }

    void ManageMovement()
    {
        if (!isBlocking)
        {
           targetSpeed = baseMovementSpeed * moveVec.magnitude;
        }
        else
        {
           targetSpeed = baseMovementSpeed / 2 * moveVec.magnitude;
        }

        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        Vector3 moveVel = transform.forward * currentSpeed + Vector3.up * velocityY;

        if (canMove)
        {
            characterController.Move(moveVel * Time.deltaTime);
        }

        characterVelocity = characterController.velocity.magnitude;

        golemState.SetFloat("playerVel", characterVelocity);

        if (characterController.isGrounded)
        {
            velocityY = 0;
        }

        velocityY += Time.deltaTime * gravity;

        if (directionVec != Vector2.zero && canMove)
        {
            float targetRotation = Mathf.Atan2(directionVec.x, directionVec.y) * Mathf.Rad2Deg;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }
    }

    public void UseAbility(int abilityNumber, Vector3 aimVec, PlayerTeam teamColor)
    {
		if (aimVec != null && (globalCooldown.cdStateEngine.currentState == globalCooldown.possibleStates[2]) && canUseAbilities)
        {
            if (aimVec != Vector3.zero)
            {
                golemAbilities[abilityNumber].CastAbility(aimVec, teamColor);
                StartCoroutine(globalCooldown.RestartCoolDownCoroutine());
            }
            else
            {
                golemAbilities[abilityNumber].CastAbility(transform.forward, teamColor);
                StartCoroutine(globalCooldown.RestartCoolDownCoroutine());
            }

        }
    }

    public void UseAttack()
    {
        if (globalCooldown.cdStateEngine.currentState == globalCooldown.possibleStates[2] && canAttack)
        {
            golemMelee.Attack();
            StartCoroutine(globalCooldown.RestartCoolDownCoroutine());
        }
    }

    public void Dodge()
    {
        if (globalCooldown.cdStateEngine.currentState == globalCooldown.possibleStates[2] && moveVec != Vector2.zero && !isDodging)
        {
            dodgeDirectionVec = new Vector3(moveVec.x, 0, moveVec.y);

            dodgeDirectionVec *= dodgeDistance;

            dodgeDestination = transform.position + dodgeDirectionVec;
            dodgeDestination.y = 0;

            Debug.Log(dodgeDestination);

            isDodging = true;

            canMove = false;
            canAttack = false;
            canUseAbilities = false;

            StartCoroutine(globalCooldown.RestartCoolDownCoroutine());
        }
    }

    void ManageDodge()
    {
        if (isDodging)
        {
            targetDodgeSpeed = dodgeSpeed * dodgeDirectionVec.magnitude;

            currentDodgeSpeed = Mathf.SmoothDamp(currentDodgeSpeed, targetDodgeSpeed, ref dodgeSmoothVelocity, dodgeSmoothTime);

            Vector3 dodgeVec = dodgeDirectionVec * currentDodgeSpeed;

            characterController.Move(dodgeVec * Time.deltaTime);

            if (Vector3.Distance(transform.position, dodgeDestination) < 2)
            {
                isDodging = false;
                canMove = true;
                canAttack = true;
                canUseAbilities = true;
            }
        }       
    }

    public void Block()
    {
        isBlocking = true;
        golemState.SetBool("isBlocking", true);
        blockIndicator.SetActive(true);
    }

    public void Unblock()
    {
        isBlocking = false;
        golemState.SetBool("isBlocking", false);
        blockIndicator.SetActive(false);
    }
    
    public void Stagger()
    {
        Debug.Log("I was Staggered! by " + gameObject.name);
        canMove = false;
        canAttack = false;
        canUseAbilities = false;
        recoveryTimer.ResetTimer(globalVariables.golemRecoveryTime);
        isStaggered = true;
        golemState.SetTrigger("isStaggered");
    }

    void ManageRecoveryTime()
    {
        if (isStaggered)
        {
            if (recoveryTimer.TimerIsDone())
            {
                canMove = true;
                canAttack = true;
                canUseAbilities = true;
                isStaggered = false;
            }
        }
    }

    void ManageIdle()
    {
        if (characterVelocity == 0)
        {
            if (idleTimer.TimerIsDone())
            {
                golemState.SetTrigger("isIdle");
            }
        }
        else
        {
            idleTimer.ResetTimer(idleTime);
            isIdle = false;
        }
    }
}
