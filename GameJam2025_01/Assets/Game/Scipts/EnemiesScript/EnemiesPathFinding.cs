using UnityEngine;
using UnityEngine.AI;

public class EnemiesPathFinding : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform finalTarget;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (finalTarget != null)
        {
            agent.SetDestination(finalTarget.position);
        }
    }

    public void SetFinalTarget(Transform target)
    {
        finalTarget = target;
        if (agent != null)
        {
            agent.SetDestination(finalTarget.position);
        }
    }
}