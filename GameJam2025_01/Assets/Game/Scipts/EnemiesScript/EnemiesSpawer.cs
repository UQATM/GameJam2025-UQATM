using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    public float spawnDistance = 2f; // Distance in front of the spawner to spawn the enemy

    // Waypoints and final destination assigned in the Inspector
    public Transform leftWaypoint;
    public Transform rightWaypoint;
    public Transform finalTarget;

    private bool spawnLeft = true; // Toggle to alternate spawns

    void Start()
    {
        // Spawn enemies repeatedly
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
        //SpawnEnemy();
        //SpawnEnemy();
    }

    void SpawnEnemy()
    {
        // Calculate a spawn position in front of the spawner
        Vector3 spawnPos = transform.position + transform.forward * spawnDistance;
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPos, transform.rotation);

        // Get the MultiPathEnemy script on the spawned enemy
        EnemiesPathFinding enemyScript = newEnemy.GetComponent<EnemiesPathFinding>();
        if (enemyScript != null)
        {
            // Alternate: assign the left waypoint on one spawn and the right on the next
            enemyScript.midWaypoint = spawnLeft ? leftWaypoint : rightWaypoint;
            enemyScript.finalTarget = finalTarget;

            // Toggle for next spawn
            spawnLeft = !spawnLeft;
        }
        else
        {
            Debug.LogWarning("Enemy prefab is missing the MultiPathEnemy script!");
        }
    }
}
