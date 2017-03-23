using System.Collections;
using UnityEngine;

public class CameraWaypoint : MonoBehaviour
{
    public GameObject lookAtTarget;

    void Start()
    {
        LookAtPoint(lookAtTarget);
    }
    void Update() {
        LookAtPoint(lookAtTarget);
    }

    void LookAtPoint(GameObject target)
    {
        transform.LookAt(target.transform);
    }
}
