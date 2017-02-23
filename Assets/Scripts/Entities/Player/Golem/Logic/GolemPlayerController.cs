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

    [Header("Debugging Values")]
    public float playerCurrentVelocity;

    public GameObject blockIndicator;

	private BasicCooldown cdAbility;

    public Animator myAnimator;

    [Header("CoolDowns")]
	private float globalCooldownTime;

	void Start () 
    {
        PlayerSetup();
	}
	
	void Update () 
    {
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
            if (canMove)
            {
                Vector3 moveVec = new Vector3(xAxis, 0, zAxis) * baseMovementSpeed * Time.deltaTime;

                /*if (moveVec.magnitude > 1)
                {
                    moveVec.Normalize();
                }*/

                Turn(moveVec.normalized);

                playerRigidbody.MovePosition(transform.position + new Vector3(xAxis, 0, zAxis) * baseMovementSpeed * Time.deltaTime);

                myAnimator.SetFloat("speed", moveVec.x + moveVec.z);
                myAnimator.SetBool("isMoving", true);
            }  
        }       
    }

    void Turn(Vector3 lookVec)
    {
        Quaternion desiredRotation = Quaternion.LookRotation(lookVec);

        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * turnSpeed);
    }

    void TurnToCast(Vector3 aimVec)
    {
        Quaternion desiredRotation = Quaternion.LookRotation(aimVec);

        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * turnCastSpeed);
    }

    public void UseAbility(int abilityNumber, Vector3 aimVec, PlayerTeam teamColor)
    {
		//Debug.Log (cdAbility.cdStateEngine.currentState.stateName + " with an ID of "+ cdAbility.cdStateEngine.currentState.stateID + " And you want " + cdAbility.possibleStates [2].stateName + " with an ID of " + cdAbility.possibleStates [2].stateID);
		if (aimVec != null && (cdAbility.cdStateEngine.currentState == cdAbility.possibleStates[2]) && golemStateMachine.combatStates == GolemStates.CombatStates.IDLE)
        {
            if (aimVec != Vector3.zero)
            {
                TurnToCast(aimVec);
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
        if (golemStateMachine.combatStates == GolemStates.CombatStates.IDLE && cdAbility.cdStateEngine.currentState == cdAbility.possibleStates[2])
        {
            playerRigidbody.AddForce(transform.forward * dodgeStrength, ForceMode.Impulse);
            StartCoroutine(cdAbility.RestartCoolDownCoroutine());
        }
    }

    public void Block()
    {
        if (golemStateMachine.combatStates == GolemStates.CombatStates.IDLE)
        {
            golemStateMachine.combatStates = GolemStates.CombatStates.BLOCK;
            isBlocking = true;
            blockIndicator.SetActive(true);
        }
    }

    public void Unblock()
    {
        golemStateMachine.combatStates = GolemStates.CombatStates.IDLE;
        isBlocking = false;
        blockIndicator.SetActive(false);
    }
}
