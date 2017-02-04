using System.Collections;
using UnityEngine;

[RequireComponent(typeof (GolemInputManager))]
[System.Serializable]
public class GolemPlayerController : GolemStats 
{
    private GolemInputManager golemInputManager;
    private GolemBaseWeapon golemBaseWeapon;
    private GolemCombatStateMachine golemCombatStateMachine;

    private Rigidbody playerRigidbody;

    private float xAxis;
    private float zAxis;

    [Header("Player Turning Attributes")]
    public float turnSpeed;

    [Header("Ground Check Attributes")]
    public float groundCheckLength;

    public bool isGrounded;

    [Header("Debugging Values")]
    public float playerCurrentVelocity;

	private BasicCooldown cdAbility;

	[Header("CoolDowns")]
	//[SerializeField]
	private float cdGlobal;
	//[SerializeField]
	private float cdEXT;


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

        cdGlobal = GameObject.FindObjectOfType<GeneralVariables>().abilityCoolDown;

		cdAbility.cdTime = cdGlobal;

        golemCombatStateMachine = GetComponent<GolemCombatStateMachine>();

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
		if (aimVec != null && (cdAbility.cdStateEngine.currentState == cdAbility.possibleStates[2]) && golemCombatStateMachine.combatStates == GolemCombatStateMachine.CombatStates.IDLE)
        {
            if (aimVec != Vector3.zero)
            {
                golemAbilities[abilityNumber].CastAbility(aimVec, teamColor);
            }
            else
            {
                golemAbilities[abilityNumber].CastAbility(transform.forward, teamColor);
            }
        }
    }

    public void UseQuickAttack()
    {
        if (cdAbility.cdStateEngine.currentState == cdAbility.possibleStates[2] && golemCombatStateMachine.combatStates == GolemCombatStateMachine.CombatStates.IDLE)
        {
            golemBaseWeapon.QuickAttack();
        }
    }

    public void Dodge()
    {
        if (golemCombatStateMachine.combatStates == GolemCombatStateMachine.CombatStates.IDLE)
        {
            playerRigidbody.AddForce(transform.forward * dodgeStrength, ForceMode.Impulse);
        }
    }

    void GroundCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.down, groundCheckLength))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
