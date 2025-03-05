using UnityEngine;
using UnityEngine.AI;

public class EnemiesPathFinding : MonoBehaviour
{
    private NavMeshAgent agent;

    // These will be assigned by the spawner
    [HideInInspector] public Transform midWaypoint;
    [HideInInspector] public Transform finalTarget;

    private bool headingToMid = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Set the initial destination to the mid-waypoint (left or right)
        if (midWaypoint != null)
        {
            agent.SetDestination(midWaypoint.position);
        }
        else
        {
            Debug.LogWarning("MidWaypoint is not assigned!");
        }
    }

    void Update()
    {
        if (headingToMid)
        {
            // If we've arrived at the mid-waypoint, switch to the final target
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                headingToMid = false;
                if (finalTarget != null)
                {
                    agent.SetDestination(finalTarget.position);
                }
                else
                {
                    Debug.LogWarning("Final target is not assigned!");
                }
            }
        }
    }
}
