using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicCameraController : MonoBehaviour
{
    [Header("Camera Waypoints")]
    public List<GameObject> cameraWaypointList;

    public GameObject targetObject;

    [Header("Camera Point Curves")]
    public List<AnimationCurve> cameraMovementAnimationCurves;
    public List<AnimationCurve> cameraRotationAnimationCurves;

    [Header("Camera Waypoint Timings")]
    public List<float> cameraPointTimes;

    [Header("Debug Attributes")]
    private Vector3 startPos;
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

        StartRoute();
    }

    void MoveCamera()
    {
        if (isActive)
        {
            transform.position = Vector3.Slerp(startPos, targetPoint.transform.position, targetMovementAnimationCurve.Evaluate(t / targetTiming));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetPoint.transform.rotation, targetMovementAnimationCurve.Evaluate(o / targetTiming));

            t += Time.deltaTime / targetTiming;
            o += Time.deltaTime / targetTiming;

            if (transform.position == targetPoint.transform.position)
            {
                isActive = false;

                t = 0;
                o = 0;

                FindNextPoint();
            }
        }
    }

    void StartRoute()
    {
        startPos = transform.position;
        targetPoint = cameraWaypointList[0];

        targetMovementAnimationCurve = cameraMovementAnimationCurves[0];
        targetRotationAnimationCurve = cameraRotationAnimationCurves[0];

        targetTiming = cameraPointTimes[0];

        isActive = true;
    }

    void FindNextPoint()
    {
        if (CanMoveToNextPosition())
        {
            startPos = targetObject.transform.position;
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
