using System.Collections;
using UnityEngine;

[RequireComponent(typeof (GolemInputManager))]
[System.Serializable]
public class GolemPlayerController : GolemStats 
{
    private TimerClass idleTimer;

    private GolemInputManager golemInputManager;
    private GolemBaseWeapon golemBaseWeapon;

    private Animator golemState;

    private Rigidbody playerRigidbody;

    private float xAxis;
    private float zAxis;

    [Header("Debugging Values")]
    public float playerCurrentVelocity;

    public float idleTime;

    public GameObject blockIndicator;

	private BasicCooldown cdAbility;

    [Header("CoolDowns")]
	private float globalCooldownTime;

	void Start () 
    {
        PlayerSetup();
	}
	
	void Update () 
    {
        GetVelocity();

        CheckIdle();
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

        golemInputManager = GetComponent<GolemInputManager>();

        golemBaseWeapon = GetComponent<GolemBaseWeapon>();

        playerRigidbody = GetComponent<Rigidbody>();

        idleTimer = new TimerClass();

        golemState = transform.GetChild(0).GetComponent<Animator>();
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

                moveVec.Normalize();

                Turn(moveVec);

                playerRigidbody.MovePosition(transform.position + moveVec);

                idleTimer.ResetTimer(idleTime);         
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
		if (aimVec != null && (cdAbility.cdStateEngine.currentState == cdAbility.possibleStates[2]))
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
        if (cdAbility.cdStateEngine.currentState == cdAbility.possibleStates[2])
        {
            golemBaseWeapon.QuickAttack();
            StartCoroutine(cdAbility.RestartCoolDownCoroutine());
        }
    }

    public void Dodge()
    {
        if (cdAbility.cdStateEngine.currentState == cdAbility.possibleStates[2])
        {
            playerRigidbody.AddForce(transform.forward * dodgeStrength, ForceMode.Impulse);
            StartCoroutine(cdAbility.RestartCoolDownCoroutine());
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
        if (idleTimer.TimerIsDone())
        {
            Debug.Log("Is Idle");
        }
    }
}
