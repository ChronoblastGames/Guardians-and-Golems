using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicCameraController : MonoBehaviour
{
    [Header("Camera Waypoints")]
    public List<GameObject> cameraWaypointList;

    [Header("Camera Point Curves")]
    public List<AnimationCurve> cameraAnimationCurves;

    [Header("Camera Waypoint Timings")]
    public List<float> cameraPointTimes;

    [Header("Debug Attributes")]
    private int waypointNumber = 0;
    private int numberOfWaypoints = 0;

    public bool isActive;

    private void Start()
    {
        CameraSetup();
    }

    void CameraSetup()
    {
        numberOfWaypoints = cameraWaypointList.Count;
    }

    void FindNextPoint()
    {
        if (waypointNumber++ <= numberOfWaypoints)
        {
            Debug.Log(waypointNumber);
            waypointNumber++;

            GameObject newPoint = cameraWaypointList[waypointNumber];
            AnimationCurve newAnimationCurve = cameraAnimationCurves[waypointNumber];
            float newPointTime = cameraPointTimes[waypointNumber];

            StartCoroutine(MoveToNextPoint(newPoint, newAnimationCurve, newPointTime));

        }
        else
        {
            Debug.Log("Reach end of Route!");
        }
    }

    private IEnumerator MoveToNextPoint(GameObject nextPoint, AnimationCurve pointCurve, float pointTime)
    {
        isActive = true;

        float moveTimer = 0;

        while (moveTimer <= pointTime)
        {
            moveTimer += Time.deltaTime / pointTime;

            float moveSpeed = pointTime * pointCurve.Evaluate(moveTimer);

            transform.position = Vector3.Lerp(transform.position, nextPoint.transform.position, moveSpeed);

            transform.rotation = Quaternion.Lerp(transform.rotation, nextPoint.transform.rotation, moveSpeed);
        }

        isActive = false;

        FindNextPoint();

        yield return null;
    }


}
