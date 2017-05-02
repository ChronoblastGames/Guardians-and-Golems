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
            if (!conduitController.golemInRange.Contains(PlayerTeam.RED))
            {
                conduitController.golemInRange.Add(PlayerTeam.RED);
            }
        }
        else if (golem.gameObject.CompareTag("GolemBlue"))
        {
            if (!conduitController.golemInRange.Contains(PlayerTeam.BLUE))
            {
                conduitController.golemInRange.Add(PlayerTeam.BLUE);
            }
        }
    }

    void OnTriggerExit(Collider golem)
    {
        if (golem.CompareTag("GolemRed"))
        {
            if (conduitController.golemInRange.Contains(PlayerTeam.RED))
            {
                conduitController.golemInRange.Remove(PlayerTeam.RED);
            }
        }
        else if (golem.CompareTag("GolemBlue"))
        {
            if (conduitController.golemInRange.Contains(PlayerTeam.BLUE))
            {
                conduitController.golemInRange.Remove(PlayerTeam.BLUE);
            }
        }
    }
}
