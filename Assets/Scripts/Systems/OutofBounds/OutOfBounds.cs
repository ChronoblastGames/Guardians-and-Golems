using System.Collections;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    public Vector3 resetPos;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = resetPos;
        }
    }
}
