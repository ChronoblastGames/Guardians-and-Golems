using System.Collections;
using UnityEngine;

[System.Serializable]
public class GolemPlayerController : GolemStats 
{
    private GolemInputManager golemInputManager;

    private Rigidbody playerRigidbody;

    private float xAxis;
    private float zAxis;

    [Header("Debug")]
    public bool controlType;

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
}
