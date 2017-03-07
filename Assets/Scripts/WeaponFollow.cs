using System.Collections;
using UnityEngine;

public class WeaponFollow : MonoBehaviour
{
    public GameObject targetObject;

    void Update()
    {
        FollowObject();
    }

    void FollowObject()
    {
        if (targetObject != null)
        {
            transform.position = targetObject.transform.position;
        }
    }
}
