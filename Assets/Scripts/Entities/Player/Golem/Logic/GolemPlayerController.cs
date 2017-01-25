using System.Collections;
using UnityEngine;

[System.Serializable]
public class GolemPlayerController : GolemStats 
{
    private GolemInputManager golemInputManager;

    private Rigidbody playerRigidbody;

    private float xAxis;
    private float zAxis;

    [Header("Player Ability Attributes")]
    public GolemAbility[] Abilities;

    [Header("Player Turning Attributes")]
    public float turnSpeed;

	void Start () 
    {
        PlayerSetup();
	}
	
	void Update () 
    {
		
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
            Vector3 moveVec = new Vector3(xAxis, 0, zAxis) * baseMovementSpeed * Time.deltaTime;

            if (moveVec.magnitude > 1)
            {
                moveVec.Normalize();
            }

            Turn(moveVec);

            playerRigidbody.MovePosition(transform.position + moveVec);
        }       
    }

    void Turn(Vector3 lookVec)
    {
        Quaternion desiredRotation = Quaternion.LookRotation(lookVec);

        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * turnSpeed);
    }       
}
