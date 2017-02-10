using System.Collections;
using UnityEngine;

[RequireComponent(typeof (GolemInputManager))]
[System.Serializable]
public class GolemPlayerController : GolemStats 
{
    private GolemInputManager golemInputManager;
    private GolemBaseWeapon golemBaseWeapon;
    private GolemStates golemStateMachine;

    private Rigidbody playerRigidbody;

    private float xAxis;
    private float zAxis;

    [Header("Player Turning Attributes")]
    public float turnSpeed;

    [Header("Ground Check Attributes")]
    public float groundCheckLength;

    public bool isGrounded;

    public LayerMask GroundMask;

    [Header("Debugging Values")]
    public float playerCurrentVelocity;

    public GameObject blockIndicator;

	private BasicCooldown cdAbility;

	[Header("CoolDowns")]
	private float globalCooldownTime;
	private float cdEXT; //?

	void Start () 
    {
        PlayerSetup();
	}
	
	void Update () 
    {
        GroundCheck();

        GetVelocity();
	}

    private void FixedUpdate()
    {
        GatherInput();
    }

    void PlayerSetup()
    {
		cdAbility = new BasicCooldown ();

        globalCooldownTime = GameObject.FindObjectOfType<GeneralVariables>().globalCooldown;

		cdAbility.cdTime = globalCooldownTime;

        golemStateMachine = GetComponent<GolemStates>();

        golemInputManager = GetComponent<GolemInputManager>();

        golemBaseWeapon = GetComponent<GolemBaseWeapon>();

        playerRigidbody = GetComponent<Rigidbody>();
    }

    void GetVelocity()
    {
        playerCurrentVelocity = playerRigidbody.velocity.magnitude;
    }

    void GatherInput()
    {
        xAxis = golemInputManager.xAxis;
        zAxis = golemInputManager.zAxis;

        Move(xAxis, zAxis);
    }

    void Move(float xAxis, float zAxis)
    {
        if (xAxis != 0 || zAxis != 0)
        {
            Vector3 moveVec = new Vector3(xAxis, 0, zAxis) * baseMovementSpeed * Time.deltaTime;

            if (moveVec.magnitude > 1)
            {
                moveVec.Normalize();
            }

            Turn(moveVec);

            playerRigidbody.MovePosition(transform.position + new Vector3(xAxis, 0, zAxis) * baseMovementSpeed * Time.deltaTime);
        }       
    }

    void Turn(Vector3 lookVec)
    {
        Quaternion desiredRotation = Quaternion.LookRotation(lookVec);

        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * turnSpeed);
    }

    public void UseAbility(int abilityNumber, Vector3 aimVec, string teamColor)
    {
		//Debug.Log (cdAbility.cdStateEngine.currentState.stateName + " with an ID of "+ cdAbility.cdStateEngine.currentState.stateID + " And you want " + cdAbility.possibleStates [2].stateName + " with an ID of " + cdAbility.possibleStates [2].stateID);
		if (aimVec != null && (cdAbility.cdStateEngine.currentState == cdAbility.possibleStates[2]) && golemStateMachine.combatStates == GolemStates.CombatStates.IDLE)
        {
            if (aimVec != Vector3.zero)
            {
                golemAbilities[abilityNumber].CastAbility(aimVec, teamColor);
                StartCoroutine(cdAbility.RestartCoolDownCoroutine());
            }
            else
            {
                golemAbilities[abilityNumber].CastAbility(transform.forward, teamColor);
                StartCoroutine(cdAbility.RestartCoolDownCoroutine());
            }

        }
    }

    public void UseQuickAttack()
    {
        if (cdAbility.cdStateEngine.currentState == cdAbility.possibleStates[2] && golemStateMachine.combatStates == GolemStates.CombatStates.IDLE)
        {
            golemBaseWeapon.QuickAttack();
            StartCoroutine(cdAbility.RestartCoolDownCoroutine());
        }
    }

    public void Dodge()
    {
        if (golemStateMachine.combatStates == GolemStates.CombatStates.IDLE)
        {
            playerRigidbody.AddForce(transform.forward * dodgeStrength, ForceMode.Impulse);
        }
    }

    public void Block()
    {
        if (golemStateMachine.combatStates == GolemStates.CombatStates.IDLE)
        {
            blockIndicator.SetActive(true);
        }
    }

    public void Unblock()
    {
        blockIndicator.SetActive(false);
    }

    void GroundCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.down, groundCheckLength, GroundMask))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
