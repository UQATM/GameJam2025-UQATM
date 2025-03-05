using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class EnemiesPathFinding : MonoBehaviour
{
    private NavMeshAgent agent;
    private List<Transform> pathWaypoints;
    private int currentWaypointIndex;
    private float repathTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InitializePath();
    }

    void Update()
    {
        if (pathWaypoints == null || currentWaypointIndex >= pathWaypoints.Count) return;

        // Check if reached current waypoint
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex < pathWaypoints.Count)
            {
                agent.SetDestination(pathWaypoints[currentWaypointIndex].position);
            }
        }

        // Periodic repathing to avoid stuck enemies
        repathTimer += Time.deltaTime;
        if (repathTimer > 1f)
        {
            agent.SetDestination(pathWaypoints[currentWaypointIndex].position);
            repathTimer = 0;
        }
    }

    public void SetPath(List<Transform> path)
    {
        pathWaypoints = path;
        InitializePath();
    }

    private void InitializePath()
    {
        currentWaypointIndex = 0;
        if (agent != null && pathWaypoints != null && pathWaypoints.Count > 0)
        {
            agent.SetDestination(pathWaypoints[0].position);
        }
    }
}