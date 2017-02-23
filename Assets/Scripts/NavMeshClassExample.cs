using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshClassExample : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    public Transform targetObject;

    void Start()
    {
        StartPathfinding();
    }

    void StartPathfinding()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (targetObject != null)
        {
            navMeshAgent.destination = targetObject.position;
        }
    }

}
