using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicCameraController : MonoBehaviour
{
    [Header("Camera Waypoints")]
    public List<GameObject> cameraWaypointList;

    [Header("Camera Point Curves")]
    public List<AnimationCurve> cameraMovementAnimationCurves;
    public List<AnimationCurve> cameraRotationAnimationCurves;

    [Header("Camera Waypoint Timings")]
    public float startDelay;

    public List<float> cameraPointTimes;

    [Header("Debug Attributes")]
    private Vector3 startPos;
    private Quaternion startRotation;

    private GameObject targetPoint;

    private AnimationCurve targetMovementAnimationCurve;
    private AnimationCurve targetRotationAnimationCurve;

    private float targetTiming;

    private int waypointNumber = 0;
    private int numberOfWaypoints = 0;

    private float t;
    private float o;

    public bool isActive;
   
    private void Start()
    {
        CameraSetup();
    }

    void FixedUpdate()
    {
        MoveCamera();
    }

    void CameraSetup()
    {
        numberOfWaypoints = cameraWaypointList.Count;

        StartCoroutine(StartRoute(startDelay));
    }

    void MoveCamera()
    {
        if (isActive)
        {
            transform.position = Vector3.Lerp(startPos, targetPoint.transform.position, targetMovementAnimationCurve.Evaluate(t / targetTiming));
            transform.rotation = Quaternion.Slerp(startRotation, targetPoint.transform.rotation, targetMovementAnimationCurve.Evaluate(o / targetTiming));

            t += Time.fixedDeltaTime / targetTiming;
            o += Time.fixedDeltaTime / targetTiming;


            Vector3 interceptVec = targetPoint.transform.position - transform.position;

            if (interceptVec.magnitude < 0.20f)
            {
                isActive = false;

                t = 0;
                o = 0;

                FindNextPoint();
            }
        }
    }

    private IEnumerator StartRoute(float waitTime)
    {
        if (waitTime > 0)
        {
            yield return new WaitForSeconds(waitTime);
        }

        transform.position = cameraWaypointList[0].transform.position;
        transform.rotation = cameraWaypointList[0].transform.rotation;

        startPos = transform.position;
        startRotation = transform.rotation;

        targetPoint = cameraWaypointList[1];

        targetMovementAnimationCurve = cameraMovementAnimationCurves[1];
        targetRotationAnimationCurve = cameraRotationAnimationCurves[1];

        targetTiming = cameraPointTimes[1];

        isActive = true;

        yield return null;
    }

    void FindNextPoint()
    {
        if (CanMoveToNextPosition())
        {
            startPos = transform.position;
            startRotation = transform.rotation;

            waypointNumber++;

            targetPoint = cameraWaypointList[waypointNumber];

            targetMovementAnimationCurve = cameraMovementAnimationCurves[waypointNumber];
            targetRotationAnimationCurve = cameraRotationAnimationCurves[waypointNumber];

            targetTiming = cameraPointTimes[waypointNumber];

            isActive = true;
        }
        else
        {
            Debug.Log("Reached end of Route");
        }
    }

    private bool CanMoveToNextPosition()
    {
        int nextPointNumber = waypointNumber + 1;

        if (nextPointNumber <= numberOfWaypoints)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
