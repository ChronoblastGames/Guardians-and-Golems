using System.Collections;
using UnityEngine;

public class OrbAreaTrigger : MonoBehaviour
{
    private OrbController orbController;

    void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        orbController = transform.parent.GetComponent<OrbController>();
    }

    void OnTriggerEnter(Collider golem)
    {
        if (golem.CompareTag("GolemRed"))
        {
            orbController.isBeingAssistedByRedGolem = true;
        }
        else if (golem.CompareTag("GolemBlue"))
        {
            orbController.isBeingAssistedByBlueGolem = true;
        }
    }

    void OnTriggerExit(Collider golem)
    {
        if (golem.CompareTag("GolemRed"))
        {
            orbController.isBeingAssistedByRedGolem = false;
        }
        else if (golem.CompareTag("GolemBlue"))
        {
            orbController.isBeingAssistedByBlueGolem = false;
        }
    }
}
