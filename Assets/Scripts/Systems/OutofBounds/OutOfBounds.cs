using System.Collections;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GolemRed") || other.CompareTag("GolemBlue"))
        {
            Vector3 resetPos = other.transform.position;
            resetPos.y = 0;
           
            other.transform.position = resetPos;
        }
    }
}
