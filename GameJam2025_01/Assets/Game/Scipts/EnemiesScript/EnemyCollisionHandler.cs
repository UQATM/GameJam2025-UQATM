using UnityEngine;

public class EnemyCollisionHandler : MonoBehaviour
{
    [SerializeField] private string obstacleTag = "Obstacle";
    [SerializeField] private string baseTag = "Base";
    [SerializeField] private int collisionDamage = 9999; // Insta-kill on collision

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(obstacleTag) || other.CompareTag(baseTag))
        {
            EnemyHealth health = GetComponent<EnemyHealth>();
            if (health != null)
                health.TakeDamage(collisionDamage); // Kill immediately
        }
    }
}