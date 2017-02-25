using System.Collections;
using UnityEngine;

[RequireComponent(typeof (GolemInputManager))]
[System.Serializable]
public class GolemPlayerController : GolemStats 
{
    private TimerClass idleTimer;
    private BasicCooldown globalCooldown;
    private GolemInputManager golemInputManager;
    private CharacterController characterController;
    private GolemBaseWeapon golemBaseWeapon;

    private Animator golemState;

    [Header("Player Movement Attributes")]
    public float currentSpeed;
    public float speedSmoothTime = 0.1f;
    private float speedSmoothVelocity;
    public float characterVelocity;

    public float idleTime;

    public bool isIdle;

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
        ManageMovement();
        CheckIdle();
	}

    void PlayerSetup()
    {
		globalCooldown = new BasicCooldown();

        globalCooldownTime = GameObject.FindObjectOfType<GeneralVariables>().globalCooldown;

		globalCooldown.cdTime = globalCooldownTime;

        golemInputManager = GetComponent<GolemInputManager>();

        golemBaseWeapon = GetComponent<GolemBaseWeapon>();

        characterController = GetComponent<CharacterController>();

        idleTimer = new TimerClass();

        golemState = transform.GetChild(0).GetComponent<Animator>();

        idleTimer.ResetTimer(idleTime);
    }

    void GatherInput()
    {
        moveVec = golemInputManager.moveVec;
        directionVec = golemInputManager.moveDirection;
    }

    void ManageMovement()
    {
        float targetSpeed = baseMovementSpeed * moveVec.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        velocityY += Time.deltaTime * gravity;

        Vector3 moveVel = transform.forward * currentSpeed + Vector3.up * velocityY;

        characterController.Move(moveVel * Time.deltaTime);

        characterVelocity = characterController.velocity.magnitude;

        if (characterController.isGrounded)
        {
            velocityY = 0;
        }

        if (directionVec != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(directionVec.x, directionVec.y) * Mathf.Rad2Deg;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }
    }


    public void UseAbility(int abilityNumber, Vector3 aimVec, PlayerTeam teamColor)
    {
		if (aimVec != null && (globalCooldown.cdStateEngine.currentState == globalCooldown.possibleStates[2]))
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

    public void UseQuickAttack()
    {
        if (globalCooldown.cdStateEngine.currentState == globalCooldown.possibleStates[2])
        {
            golemBaseWeapon.Attack();
            StartCoroutine(globalCooldown.RestartCoolDownCoroutine());
        }
    }

    public void Dodge()
    {
        if (globalCooldown.cdStateEngine.currentState == globalCooldown.possibleStates[2])
        {
            
        }
    }

    public void Block()
    {
        isBlocking = true;
        blockIndicator.SetActive(true);
    }

    public void Unblock()
    {
        isBlocking = false;
        blockIndicator.SetActive(false);
    }

    void CheckIdle()
    {
        if (characterVelocity == 0)
        {
            if (idleTimer.TimerIsDone())
            {
                isIdle = true;
            }
        }
        else
        {
            idleTimer.ResetTimer(idleTime);
            isIdle = false;
        }
    }
}
