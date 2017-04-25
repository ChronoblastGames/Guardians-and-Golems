using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingWallController : MonoBehaviour
{
    [Header("Wall Attributes")]
    public float startingDelay;
    [Space(10)]
    public float wallMoveValue;
    [Space(10)]
    public float wallMoveTime;
    private float t;

    private bool canWallMove = false;

    private Vector3 movePos;

    private void Start()
    {
        movePos = transform.position;

        movePos.y -= wallMoveValue;

        StartCoroutine(WallDelay(startingDelay));         
    }

    private void FixedUpdate()
    {
        MoveWall();
    }

    private void MoveWall()
    {
        if (canWallMove)
        {
            transform.position = Vector3.Lerp(transform.position, movePos, t);

            t += Time.fixedDeltaTime / wallMoveTime;

            if (transform.position == movePos)
            {
                Destroy(gameObject, 1f);
            }
        }
    }

    private IEnumerator WallDelay(float wallDelay)
    {
        yield return new WaitForSeconds(wallDelay);

        canWallMove = true;
    }
}
