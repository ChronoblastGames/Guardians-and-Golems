﻿using System.Collections;
using UnityEngine;

[RequireComponent(typeof (GolemInputController))]
[System.Serializable]
public class GolemPlayerController : GolemStats 
{
    private MapManager mapManager;

    private CommandManager commandManager;

    private CrystalManager crystalManager;

    [HideInInspector]
    public CharacterController characterController;

    private GolemInputController golemInputController;
    private GolemResources golemResources;
    private GolemMelee golemMelee;
    private GolemCooldownManager golemCooldown;

    private GlobalVariables globalVariables;

    private Animator golemState;

    [Header("Timers")]
    private TimerClass idleTimer;
    private TimerClass recoveryTimer;

    [Header("Player Movement Attributes")]
    public PlayerTeam playerColor = PlayerTeam.NONE;

    public float movementSpeed = 0.0f;
    public float currentSpeed;
    public float speedSmoothTime = 0.1f;
    private float targetSpeed;
    private float speedSmoothVelocity;
    public float characterVelocity;

    [Header("Player Dodge Attributes")]
    public GolemDodgeAbilityBase golemDodge;

    [HideInInspector]
    public bool isStaggered;

    private Vector2 moveVec;
    private Vector2 directionVec;

    [Header("Player Turning Attributes")]
    public float abilitySmoothTime = 0.1f;
    public float turnSmoothTime = 0.2f;
    public float attackTurnSmoothTime = 0.3f;
    private float turnSmoothVelocity;

    [Header("Player Gravity Attributes")]
    public float gravity = -12f;
    private float velocityY;

    [Header("Player Booleans")]
    public bool canRotate = true;
    public bool canMove = true;
    public bool canDodge = true;
    public bool canUseAbilities = true;
    public bool canAttack = true;
    public bool isDodging = false;
    public bool isAttacking = false;
    public bool isCastingAbility = false;
    public bool isSlowed = false;

    public bool isDead = false;

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
        ManageRotation();
        ManageRecoveryTime();
    }

    void PlayerSetup()
    {
        commandManager = GameObject.FindGameObjectWithTag("CommandManager").GetComponent<CommandManager>();

        crystalManager = GameObject.FindGameObjectWithTag("CrystalManager").GetComponent<CrystalManager>();

        mapManager = GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>();

        characterController = GetComponent<CharacterController>();

        golemInputController = GetComponent<GolemInputController>();

        golemResources = GetComponent<GolemResources>();

        golemMelee = GetComponent<GolemMelee>();

        golemCooldown = GetComponent<GolemCooldownManager>();

        globalVariables = GameObject.FindGameObjectWithTag("GlobalVariables").GetComponent<GlobalVariables>();

        recoveryTimer = new TimerClass();

        golemState = GetComponent<Animator>();

        playerColor = golemInputController.playerTeam;

        movementSpeed = baseMovementSpeed;
    }

    void GatherInput()
    {
        moveVec = golemInputController.moveVec;
        directionVec = golemInputController.moveDirection;
    }

    void ManageMovement()
    {
        targetSpeed = movementSpeed * moveVec.magnitude;

        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        Vector3 moveVel = transform.forward * currentSpeed + Vector3.up * velocityY;

        if (canMove && !isDead)
        {
            characterController.Move(moveVel * Time.fixedDeltaTime);
        }

        characterVelocity = characterController.velocity.magnitude;

        golemState.SetFloat("playerVel", characterVelocity);

        if (characterController.isGrounded)
        {
            velocityY = 0;
        }
        else
        {
            velocityY += Time.fixedDeltaTime * gravity;
        }
    }

    void ManageRotation()
    {
        if (directionVec != Vector2.zero && canRotate && !isDead)
        {
            if (isAttacking)
            {
                float targetRotation = Mathf.Atan2(directionVec.x, directionVec.y) * Mathf.Rad2Deg;
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, attackTurnSmoothTime);
            }
            else
            {
                float targetRotation = Mathf.Atan2(directionVec.x, directionVec.y) * Mathf.Rad2Deg;
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
            }
        }
        else if (isCastingAbility)
        {
            if (golemInputController.aimVec != Vector3.zero)
            {
                float targetRotation = Mathf.Atan2(golemInputController.aimVec.x, golemInputController.aimVec.z) * Mathf.Rad2Deg;

                if (targetRotation < 0)
                {
                    targetRotation += 360;
                }

                transform.eulerAngles = Vector3.up * Mathf.SmoothDamp(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, abilitySmoothTime);
            }
        }
    }

    public void UseAbility(int abilityNumber, PlayerTeam teamColor, float holdTime)
    {
        if (canUseAbilities && golemCooldown.GlobalCooldownReady() && golemCooldown.CanUseAbility(abilityNumber) && !isDead)
        {
            if (golemResources.CanCast(golemAbilities[abilityNumber].GetComponent<GolemAbilityCreate>().healthCost) && crystalManager.TryCast(golemAbilities[abilityNumber].GetComponent<GolemAbilityCreate>().crystalCost, teamColor, PlayerType.GOLEM))
            {
                golemState.SetTrigger("UseAbility");
                golemState.SetTrigger("Ability" + (abilityNumber + 1));
                golemAbilities[abilityNumber].CastAbility(teamColor, holdTime, gameObject);
                golemCooldown.QueueGlobalCooldown();
                golemCooldown.QueueAbilityCooldown(abilityNumber);
            }
        }
    }

    public void UseAttack()
    {
        if (canAttack && !isDead)
        {
            golemMelee.QueueAttack();

            golemCooldown.QueueGlobalCooldown();
        }
    }

    public void DodgeSetup()
    {
        if (!isDodging && canDodge && golemCooldown.GlobalCooldownReady() && golemCooldown.DodgeCooldownReady() && !isDead)
        {
            Vector3 dodgeDirectionVec;

            if (moveVec == Vector2.zero)
            {
                dodgeDirectionVec = transform.forward;
                dodgeDirectionVec.y = 0;
            }
            else
            {
                dodgeDirectionVec = new Vector3(moveVec.x, 0, moveVec.y).normalized;
            }
           
            golemState.SetTrigger("isDodge");

            canMove = false;
            canDodge = false;
            canAttack = false;
            canRotate = false;
            canUseAbilities = false;
            isDodging = true;

            golemCooldown.QueueGlobalCooldown();
            golemCooldown.QueueDodgeCooldown();

            golemDodge.StartDodge(dodgeDirectionVec);
        }
    } 

    public void StopDodge()
    {      
        canDodge = true;
        canMove = true;
        canRotate = true;
        canUseAbilities = true;
        canAttack = true;
        isDodging = false;
    }
  
    public void Stagger()
    {
        canMove = false;
        canAttack = false;
        canUseAbilities = false;
        isStaggered = true;

        recoveryTimer.ResetTimer(globalVariables.golemRecoveryTime);
        golemState.SetTrigger("isStaggered");
    }

    void ManageRecoveryTime()
    {
        if (isStaggered)
        {
            if (recoveryTimer.TimerIsDone())
            {
                RecoverFromStagger();
            }
        }
    }

    void RecoverFromStagger()
    {
        canMove = true;
        canAttack = true;
        canUseAbilities = true;
        canDodge = true;
        isStaggered = false;
    }

    public void StartMovement()
    {
        canMove = true;
        canRotate = true;
        golemState.SetBool("canMove", true);
    }

    public void StopMovement()
    {
        canMove = false;
        canRotate = false;
        golemState.SetBool("canMove", false);
    }

    public void Die()
    {
        isDead = true;

        golemState.SetTrigger("isDead");

        StopMovement();

        canAttack = false;
        canDodge = false;
        canUseAbilities = false;

        StartCoroutine(Respawn(globalVariables.golemRespawnTime));

        commandManager.GolemDeath(playerColor);
    } 

    private IEnumerator Respawn (float respawnTime)
    {
        if (respawnTime > 0)
        {
            yield return new WaitForSeconds(respawnTime);
        }

        if (playerColor == PlayerTeam.RED)
        {
            transform.position = mapManager.yellowTeamGolemSpawn.position;
            transform.rotation = mapManager.yellowTeamGolemSpawn.rotation;
        }
        else if (playerColor == PlayerTeam.BLUE)
        {
            transform.position = mapManager.blueTeamGolemSpawn.position;
            transform.rotation = mapManager.blueTeamGolemSpawn.rotation;
        }

        ResetGolem();

        yield return null;
    }

    void ResetGolem()
    {
        StartMovement();

        canAttack = true;
        canDodge = true;
        canUseAbilities = true;

        golemResources.currentHealth = golemResources.maxHealth;

        golemState.SetTrigger("Reset");

        isDead = false;

        golemResources.GolemRespawn();
    }
}
