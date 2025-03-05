using UnityEngine;

public class FieldOfViewVisualizer : MonoBehaviour
{
    public float viewRadius = 10f;
    public float viewAngle = 90f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle / 2, 0) * transform.forward * viewRadius;
        Vector3 rightBoundary = Quaternion.Euler(0, viewAngle / 2, 0) * transform.forward * viewRadius;

        // Draw FOV boundaries
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);

        // Draw the detection radius
        Gizmos.color = new Color(1, 1, 0, 0.2f); // Yellow, semi-transparent
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}