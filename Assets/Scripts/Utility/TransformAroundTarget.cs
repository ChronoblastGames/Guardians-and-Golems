using System.Collections;
using UnityEngine;

public class TransformAroundTarget : MonoBehaviour 
{
    public GameObject targetPoint;

    public float travelSpeed;

    private void Update()
    {
        RotateAround();
    }

    void RotateAround()
    {
        transform.RotateAround(targetPoint.transform.position, Vector3.forward, travelSpeed * Time.deltaTime);
    }
}
