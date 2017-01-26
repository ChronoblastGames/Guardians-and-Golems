using System.Collections;
using UnityEngine;

[RequireComponent(typeof (GolemInputManager))]
[System.Serializable]
public class GolemPlayerController : GolemStats 
{
    private GolemInputManager golemInputManager;

    private Rigidbody playerRigidbody;

    private float xAxis;
    private float zAxis;

    [Header("Ground Check Attributes")]
    public float groundCheckLength;

    public bool isGrounded;

    [Header("Debug")]
    public bool controlType;

	void Start () 
    {
        PlayerSetup();
	}
	
	void Update () 
    {
        GroundCheck();
	}

    private void FixedUpdate()
    {
        GatherInput();
    }

    void PlayerSetup()
    {
        golemInputManager = GetComponent<GolemInputManager>();

        playerRigidbody = GetComponent<Rigidbody>();
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
            if (controlType)
            {
                playerRigidbody.MovePosition(transform.position + new Vector3(xAxis, 0, zAxis) * baseMovementSpeed * Time.deltaTime);
            }
            else
            {
                playerRigidbody.AddForce(new Vector3(xAxis, 0, zAxis) * baseMovementSpeed * Time.deltaTime, ForceMode.Impulse);
            }
        }       
    }

    public void UseAbility(int abilityNumber)
    {
        Vector3 forwardVec = transform.forward;

        GameObject newAbility = Instantiate(golemAbilities[abilityNumber], transform.position, transform.rotation) as GameObject;
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
