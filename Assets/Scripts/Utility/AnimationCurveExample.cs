using System.Collections;
using UnityEngine;

public class AnimationCurveExample : MonoBehaviour
{
    public AnimationCurve myCurve;

    public float jumpHeight = 3;
    public float jumpTime = 2;
    public float currentJumpTime;

    private bool canJump = true;
    private bool isJumping;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canJump)
            {
                StartCoroutine(Jump());
            }
        }

        if (isJumping)
        {
            currentJumpTime += (Time.deltaTime / jumpTime);

            float currentJumpHeight = myCurve.Evaluate(currentJumpTime) * jumpHeight;

            transform.position = new Vector3(0, currentJumpHeight, 0);
        }
    }

    private IEnumerator Jump()
    {
        currentJumpTime = 0;

        canJump = false;
        isJumping = true;

        yield return new WaitForSeconds(jumpTime);

        isJumping = false;
        canJump = true;

        yield return null;
    }
}
