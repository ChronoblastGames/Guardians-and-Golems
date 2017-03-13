using System.Collections;
using UnityEngine;

[RequireComponent(typeof (GolemInputManager))]
[System.Serializable]
public class GolemPlayerController : GolemStats 
{
    [HideInInspector]
    public CharacterController characterController;

    private GolemInputManager golemInputManager;
    private GolemMelee golemMelee;
    private CooldownManager golemCooldown;

    private GlobalVariables globalVariables;

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


    [Header("Player Dodge Attributes")]
    public AnimationCurve dodgeCurve;

    public float dodgeTime;

    private Vector3 dodgeVec;
    private Vector3 dodgeDirectionVec;
    private Vector3 dodgeDestination;

    private IEnumerator DodgeCoroutine;

    [HideInInspector]
    public bool isStaggered;

    private Vector2 moveVec;
    private Vector2 directionVec;

    [Header("Player Turning Attributes")]
    public float turnSmoothTime = 0.2f;
    public float attackTurnSmoothTime = 0.3f;
    private float turnSmoothVelocity;

    [Header("Player Gravity Attributes")]
    public float gravity = -12f;
    private float velocityY;

    [Header("Player Booleans")]
    public bool isIdle = false;
    public bool canRotate = true;
    public bool canMove = true;
    public bool canDodge = true;
    public bool canUseAbilities = true;
    public bool canAttack = true;
    public bool canBlock = true;
    public bool isDodging = false;
    public bool isBlocking = false;
    public bool isAttacking = false;

    [Header("Debugging Values")]
    public GameObject blockIndicator;

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
        ManageIdle();
        ManageRecoveryTime();
    }

    void PlayerSetup()
    {
        characterController = GetComponent<CharacterController>();

        golemInputManager = GetComponent<GolemInputManager>();

        golemMelee = GetComponent<GolemMelee>();

        golemCooldown = GetComponent<CooldownManager>();

        globalVariables = GameObject.FindGameObjectWithTag("GlobalVariables").GetComponent<GlobalVariables>();

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
        if (isBlocking)
        {
           targetSpeed = baseMovementSpeed / 4 * moveVec.magnitude;
        }
        else if (isAttacking)
        {
            targetSpeed = baseMovementSpeed / 4 * moveVec.magnitude;
        }
        else
        {
            targetSpeed = baseMovementSpeed * moveVec.magnitude;
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
        else
        {
            velocityY += Time.deltaTime * gravity;
        }    
    }

    void ManageRotation()
    {
        if (directionVec != Vector2.zero && canRotate)
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
    }

    public void UseAbility(int abilityNumber, Vector3 aimVec, PlayerTeam teamColor, float holdTime)
    {
        if (aimVec != null && canUseAbilities && golemCooldown.GlobalCooldownReady() && golemCooldown.CanUseAbility(abilityNumber))
        {
            if (aimVec != Vector3.zero)
            {
                golemAbilities[abilityNumber].CastAbility(aimVec, teamColor, holdTime);
                golemCooldown.QueueGlobalCooldown();
                golemCooldown.QueueAbilityCooldown(abilityNumber);
            }
            else
            {
                golemAbilities[abilityNumber].CastAbility(transform.forward, teamColor, holdTime);
                golemCooldown.QueueGlobalCooldown();
                golemCooldown.QueueAbilityCooldown(abilityNumber);
            }
        }
    }

    public void UseAttack()
    {
        if (canAttack && golemCooldown.GlobalCooldownReady())
        {
            golemMelee.QueueAttack();
            golemCooldown.QueueGlobalCooldown();
        }
    }

    public void DodgeSetup()
    {
        if (!isDodging && canDodge && golemCooldown.GlobalCooldownReady())
        {
            if (moveVec == Vector2.zero)
            {
                dodgeDirectionVec = transform.forward;
                dodgeDirectionVec.y = 0;
            }
            else
            {
                dodgeDirectionVec = new Vector3(moveVec.x, 0, moveVec.y).normalized;
            }
           
            dodgeDestination = transform.position + (dodgeDirectionVec * dodgeDistance);
            dodgeDestination.y = 0;

            dodgeSpeed = dodgeDistance / dodgeTime;

            dodgeVec = (dodgeDestination - transform.position).normalized;

            dodgeDestination.y = 0;

            golemState.SetTrigger("isDodge");

            canMove = false;
            canDodge = false;
            canAttack = false;
            canRotate = false;
            canUseAbilities = false;
            isDodging = true;

            golemCooldown.QueueGlobalCooldown();
            StartCoroutine(Dodge(dodgeVec, dodgeCurve, dodgeTime));
            DodgeCoroutine = (Dodge(dodgeVec, dodgeCurve, dodgeTime));
        }
    } 

    public void StopDodging()
    {
        if (isDodging)
        {
            StopCoroutine(DodgeCoroutine);
        }
    }


    IEnumerator Dodge(Vector3 interceptVec, AnimationCurve dodgeCurve, float dodgeTime)
    {
        float dodgeTimer = 0f;

        while (dodgeTimer <= dodgeTime)
        {
            dodgeTimer += Time.deltaTime / dodgeTime;

            float dodgeCurrentSpeed = dodgeSpeed * dodgeCurve.Evaluate(dodgeTimer);

            characterController.Move(interceptVec * dodgeCurrentSpeed);

            if (dodgeTimer > 1)
            {
                ReachedEndOfDodge();
            }

            yield return null;
        }
    }

    void ReachedEndOfDodge()
    {
        DodgeCoroutine = null;

        canDodge = true;
        canMove = true;
        canRotate = true;
        canUseAbilities = true;
        canAttack = true;
        canBlock = true;
        isDodging = false;
    }

    public void Block()
    {
        if (canBlock)
        {
            isBlocking = true;
            golemState.SetBool("isBlocking", true);
            blockIndicator.SetActive(true);
        }
    }

    public void Unblock()
    {
        isBlocking = false;
        golemState.SetBool("isBlocking", false);
        blockIndicator.SetActive(false);
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
        canBlock = true;
        isStaggered = false;
    }

    void ManageIdle()
    {
        if (characterVelocity == 0)
        {
            if (idleTimer.TimerIsDone())
            {
                golemState.SetBool("isIdle", true);
            }
        }
        else if (characterVelocity != 0)
        {
            idleTimer.ResetTimer(idleTime);
            golemState.SetBool("isIdle", false);
            isIdle = false;
        }
    }
}
