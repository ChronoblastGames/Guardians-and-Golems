using System.Collections;
using UnityEngine;

[RequireComponent(typeof(GuardianInputManager))]
[System.Serializable]
public class GuardianPlayerController : GuardianStats 
{
    private GuardianInputManager guardianInputManager;

    private Rigidbody playerRigidbody;

    private float xAxis;
    private float zAxis;

    [Header("Debugging Values")]
    public float playerCurrentVelocity;
    private BasicCooldown cdAbility;

    [Header("CoolDowns")]
    private float globalCooldownTime;

    void Start()
    {
        PlayerSetup();
    }

    void Update()
    {
        GetVelocity();
    }

    private void FixedUpdate()
    {
        GatherInput();
    }

    void PlayerSetup()
    {
        cdAbility = new BasicCooldown();

        globalCooldownTime = GameObject.FindObjectOfType<GeneralVariables>().globalCooldown;

        cdAbility.cdTime = globalCooldownTime;

        guardianInputManager = GetComponent<GuardianInputManager>();

        playerRigidbody = GetComponent<Rigidbody>();
    }

    void GetVelocity()
    {
        playerCurrentVelocity = playerRigidbody.velocity.magnitude;
    }

    void GatherInput()
    {
        xAxis = guardianInputManager.xAxis;
        zAxis = guardianInputManager.zAxis;

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
        if (aimVec != null && (cdAbility.cdStateEngine.currentState == cdAbility.possibleStates[2]))
        {
            if (aimVec != Vector3.zero)
            {
                guardianAbilites[abilityNumber].CastAbility(aimVec, teamColor);
                StartCoroutine(cdAbility.RestartCoolDownCoroutine());
            }
            else
            {
                guardianAbilites[abilityNumber].CastAbility(transform.forward, teamColor);
                StartCoroutine(cdAbility.RestartCoolDownCoroutine());
            }
        }
    }
}
