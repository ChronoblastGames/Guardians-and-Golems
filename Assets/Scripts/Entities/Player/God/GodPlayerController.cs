using System.Collections;
using UnityEngine;

public class GodPlayerController : GodStats 
{
    private GodInputManager godInputManager;

    private Rigidbody playerRigidbody;

    private float xAxis;
    private float zAxis;

    [Header("Debug")]
    public bool controlType;

    void Start()
    {
        PlayerSetup();
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        GatherInput();
    }

    void PlayerSetup()
    {
        godInputManager = GetComponent<GodInputManager>();

        playerRigidbody = GetComponent<Rigidbody>();
    }

    void GatherInput()
    {
        xAxis = godInputManager.xAxis;
        zAxis = godInputManager.zAxis;

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
