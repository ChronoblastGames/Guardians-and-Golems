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

    [Header("Player Turning Attributes")]
    public float turnSpeed;

    [Header("Ground Check Attributes")]
    public float groundCheckLength;

    public bool isGrounded;

    [Header("Debug")]
    public GameObject fireBallTest;

    public float fireballSpeed;

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
        GatherPhysicsInput();
    }

    void PlayerSetup()
    {
        golemInputManager = GetComponent<GolemInputManager>();

        playerRigidbody = GetComponent<Rigidbody>();
    }

    void GatherPhysicsInput()
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

    public void UseAbility()
    {
        Vector3 forwardVec = transform.position + new Vector3(0, 0.5f, 0)  + transform.forward * 2;

        Quaternion forwardRot = transform.rotation;

        GameObject newFireball = Instantiate(fireBallTest, forwardVec, forwardRot) as GameObject;
        newFireball.GetComponent<Rigidbody>().AddForce(newFireball.transform.forward * fireballSpeed * Time.deltaTime, ForceMode.Impulse);
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
