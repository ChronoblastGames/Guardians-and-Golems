using System.Collections;
using UnityEngine;

public class ConduitAreaTrigger : MonoBehaviour
{
    private ConduitController conduitController;

    void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        conduitController = transform.parent.GetComponent<ConduitController>();
    }

    void OnTriggerEnter(Collider golem)
    {
        if (golem.gameObject.CompareTag("GolemRed"))
        {
            conduitController.isBeingAssistedByRedGolem = true;
        }
        else if (golem.gameObject.CompareTag("GolemBlue"))
        {
            conduitController.isBeingAssistedByBlueGolem = true;
        }
    }

    void OnTriggerExit(Collider golem)
    {
        if (golem.CompareTag("GolemRed"))
        {
            conduitController.isBeingAssistedByRedGolem = false;
        }
        else if (golem.CompareTag("GolemBlue"))
        {
            conduitController.isBeingAssistedByBlueGolem = false;
        }
    }
}
