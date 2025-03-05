using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemiesPathFinding : MonoBehaviour
{
    [Header("Navigation Settings")]
    [SerializeField] private float pathUpdateTolerance = 0.5f;  // Minimum distance change to trigger path update
    [SerializeField] private float centerPathBias = 0.5f;       // 0 = left edge, 1 = right edge, 0.5 = center

    private NavMeshAgent agent;
    private List<Transform> pathWaypoints;
    private int currentWaypointIndex;
    private Vector3 currentTargetPosition;
    private float originalStoppingDistance;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        originalStoppingDistance = agent.stoppingDistance;

        // Configure for smoother path following
        agent.acceleration = 25f;
        agent.angularSpeed = 360f;
        agent.radius = 0.25f;  // Reduce to prevent wall hugging
    }

    void Update()
    {
        if (pathWaypoints == null || currentWaypointIndex >= pathWaypoints.Count) return;

        // Calculate adjusted target position with center bias
        Vector3 waypointPosition = pathWaypoints[currentWaypointIndex].position;
        Vector3 pathDirection = (waypointPosition - transform.position).normalized;
        Vector3 rightOffset = Vector3.Cross(pathDirection, Vector3.up) * agent.radius * (centerPathBias - 0.5f) * 2;

        currentTargetPosition = waypointPosition + rightOffset;

        // Update destination only when significantly needed
        if (Vector3.Distance(agent.destination, currentTargetPosition) > pathUpdateTolerance)
        {
            agent.SetDestination(currentTargetPosition);
        }

        // Progress to next waypoint
        if (!agent.pathPending &&
            agent.remainingDistance <= agent.stoppingDistance + 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex < pathWaypoints.Count)
            {
                agent.stoppingDistance = originalStoppingDistance;
            }
        }
    }

    public void SetPath(List<Transform> path)
    {
        pathWaypoints = path;
        currentWaypointIndex = 0;
        agent.stoppingDistance = 0.1f;  // Tight tolerance for waypoints
        agent.SetDestination(currentTargetPosition);
    }

    void OnDrawGizmosSelected()
    {
        if (agent != null && pathWaypoints != null && currentWaypointIndex < pathWaypoints.Count)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, currentTargetPosition);
            Gizmos.DrawSphere(currentTargetPosition, 0.25f);
        }
    }
}