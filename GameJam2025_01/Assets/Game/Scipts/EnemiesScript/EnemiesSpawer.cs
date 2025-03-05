using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;  // Prefab for the enemy
    [SerializeField] private float spawnDistance = 2f;   // Distance from the spawner to spawn enemies
    [SerializeField] private float spawnInterval = 1f;   // Time interval between spawning each enemy
    [SerializeField] private Waves waveSystem;           // Reference to the Waves script

    [SerializeField] private Transform leftWaypoint;     // Left waypoint for enemy pathing
    [SerializeField] private Transform rightWaypoint;    // Right waypoint for enemy pathing
    [SerializeField] private Transform finalTarget;      // Final destination for enemy pathing

    private bool spawnLeft = true;                        // Toggle to alternate left/right waypoint assignment

    public void SpawnWave(int enemyCount, int enemyHealth)
    {
        // Debug.Log("Spawning wave with " + enemyCount + " enemies.");
        StartCoroutine(SpawnWaveCoroutine(enemyCount, enemyHealth));
    }

    private IEnumerator SpawnWaveCoroutine(int enemyCount, int enemyHealth)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy(enemyHealth);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy(int hp)
    {
        Vector3 spawnPos = transform.position + transform.forward * spawnDistance;
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPos, transform.rotation);
        // Debug.Log("Spawned enemy: " + newEnemy.name);
        EnemyHealth enemyHealthScript = newEnemy.GetComponent<EnemyHealth>();
        if (enemyHealthScript != null)
        {
            enemyHealthScript.SetHealth(hp);
            enemyHealthScript.SetWaveSystem(waveSystem);
        }

        // Reintroduce left/right waypoint assignment using the enemy's pathfinding script
        EnemiesPathFinding enemyPathScript = newEnemy.GetComponent<EnemiesPathFinding>();
        if (enemyPathScript != null)
        {
            enemyPathScript.midWaypoint = spawnLeft ? leftWaypoint : rightWaypoint;
            enemyPathScript.finalTarget = finalTarget;
            spawnLeft = !spawnLeft;
        }
    }

    public void OnWaveEnded()
    {
        // Debug.Log("Wave ended. Waiting 5 seconds before starting next wave.");
        StartCoroutine(WaveEndedCoroutine());
    }

    private IEnumerator WaveEndedCoroutine()
    {
        yield return new WaitForSeconds(5f);
        // Debug.Log("Starting next wave...");
        waveSystem.StartNextWave();
    }
}
