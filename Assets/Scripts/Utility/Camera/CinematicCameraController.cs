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
    public List<AnimationCurve> cameraZoomAnimationCurves;

    [Header("Camera Zoom Values")]
    public List<float> zoomPointTarget;

    [Header("Camera Waypoint Timings")]
    public float startDelay;

    public List<float> cameraWaitTimes;
    public List<float> cameraPointSpeedModifiers;

    [Header("Debug Attributes")]
    private Vector3 startPos;
    private Quaternion startRotation;
    private float startFOV;

    private GameObject targetPoint;

    private AnimationCurve targetMovementAnimationCurve;
    private AnimationCurve targetRotationAnimationCurve;
    private AnimationCurve targetZoomAnimationCurve;

    private float targetTiming;

    private int waypointNumber = 0;
    private int numberOfWaypoints = 0;
    private int nextPointNumber = 1;

    private float t;
    private float o;

    public bool isActive;
   
    private void Start()
    {
        CameraSetup();
    }

    void Update()
    {
        MoveCamera();
    }

    void CameraSetup()
    {
        numberOfWaypoints = cameraWaypointList.Count;

        transform.position = cameraWaypointList[0].transform.position;
        transform.rotation = cameraWaypointList[0].transform.rotation;

        StartCoroutine(StartRoute(startDelay));
    }

    void MoveCamera()
    {
        if (isActive)
        {
            transform.position = Vector3.Lerp(startPos, targetPoint.transform.position, targetMovementAnimationCurve.Evaluate(t / targetTiming));
            transform.rotation = Quaternion.Lerp(startRotation, targetPoint.transform.rotation, targetMovementAnimationCurve.Evaluate(o / targetTiming));
            Camera.main.fieldOfView = Mathf.Lerp(startFOV, zoomPointTarget[waypointNumber], targetZoomAnimationCurve.Evaluate(t / targetTiming));

            t += Time.deltaTime / targetTiming;
            o += Time.deltaTime / targetTiming;


            Vector3 interceptVec = targetPoint.transform.position - transform.position;

            if (interceptVec.magnitude < 0.20f)
            {
                isActive = false;

                t = 0;
                o = 0;

                StartCoroutine(FindNextPoint());
            }
        }
    }

    private IEnumerator StartRoute(float waitTime)
    {
        if (waitTime > 0)
        {
            yield return new WaitForSeconds(waitTime);
        }

        startPos = transform.position;
        startRotation = transform.rotation;
        startFOV = Camera.main.fieldOfView;

        targetPoint = cameraWaypointList[1];

        targetMovementAnimationCurve = cameraMovementAnimationCurves[1];
        targetRotationAnimationCurve = cameraRotationAnimationCurves[1];
        targetZoomAnimationCurve = cameraZoomAnimationCurves[1];

        targetTiming = cameraPointSpeedModifiers[1];

        isActive = true;

        yield return null;
    }

    private IEnumerator FindNextPoint()
    {
        if (CanMoveToNextPosition())
        {
            if (cameraWaitTimes[waypointNumber] > 0)
            {
                yield return new WaitForSeconds(cameraWaitTimes[waypointNumber]);
            }

            startPos = transform.position;
            startRotation = transform.rotation;

            waypointNumber++;

            targetPoint = cameraWaypointList[waypointNumber];

            targetMovementAnimationCurve = cameraMovementAnimationCurves[waypointNumber];
            targetRotationAnimationCurve = cameraRotationAnimationCurves[waypointNumber];
            targetZoomAnimationCurve = cameraZoomAnimationCurves[waypointNumber];

            targetTiming = cameraPointSpeedModifiers[waypointNumber];

            isActive = true;

            yield return null;
        }
    }

    private bool CanMoveToNextPosition()
    {
        nextPointNumber++;

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
