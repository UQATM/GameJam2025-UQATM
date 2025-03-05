using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class EnemiesPathFinding : MonoBehaviour
{
    private NavMeshAgent agent;
    private List<Transform> pathWaypoints;
    private int currentWaypointIndex = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (pathWaypoints != null && pathWaypoints.Count > 0)
        {
            agent.SetDestination(pathWaypoints[0].position);
        }
    }

    void Update()
    {
        if (pathWaypoints == null || currentWaypointIndex >= pathWaypoints.Count) return;

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex < pathWaypoints.Count)
            {
                agent.SetDestination(pathWaypoints[currentWaypointIndex].position);
            }
        }
    }

    public void SetPath(List<Transform> path)
    {
        pathWaypoints = path;
        currentWaypointIndex = 0;
        if (agent != null && pathWaypoints.Count > 0)
        {
            agent.SetDestination(pathWaypoints[0].position);
        }
    }
}