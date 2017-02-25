using System.Collections;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour 
{
    private CharacterController charController;

    [Header("Player Input")]
    private float xAxis;
    private float zAxis;
    public Vector2 inputVec;
    public Vector2 inputDirection;

    [Header("Player Attributes")]
    private float currentSpeed;
    public float speedSmoothTime = 0.1f;
    private float speedSmoothVelocity;
    public float walkSpeed = 2;
    public float runSpeed = 5;
    public bool isRunning;

    public float turnSmoothTime = 0.2f;
    private float turnSmoothVelocity;

    public float gravity = -12;
    public float velocityY;

    private void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        GetInput();
        ManageMovement();
    }

    void GetInput()
    {
        xAxis = Input.GetAxis("HorizontalPlayer1Win");
        zAxis = Input.GetAxis("VerticalPlayer1Win");
        inputVec = new Vector2(xAxis, zAxis);
        inputDirection = inputVec.normalized;

        isRunning = Input.GetKeyDown("joystick 1 button 0");

        float targetSpeed = ((isRunning) ? runSpeed : walkSpeed) * inputDirection.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);
    }

    void ManageMovement()
    {
        velocityY += Time.deltaTime * gravity;

        Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;

        charController.Move(velocity * Time.deltaTime);
        currentSpeed = new Vector2(charController.velocity.x, charController.velocity.z).magnitude;

        if (charController.isGrounded)
        {
            velocityY = 0;
        }

        if (inputDirection != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.y) * Mathf.Rad2Deg;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime); // Using Trig, we find the angle of our inputdirection, using Atan2 to manage the math.
        }
    }
}
