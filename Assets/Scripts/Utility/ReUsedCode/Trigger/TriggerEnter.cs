using UnityEngine;
using System.Collections;

public class TriggerEnter : MonoBehaviour {

   

    // Use this for initialization
    void Start () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

        }
    }
    void OnTriggerStay(Collider other)
    {

    }
    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player")
        {

        }
    }
 
}