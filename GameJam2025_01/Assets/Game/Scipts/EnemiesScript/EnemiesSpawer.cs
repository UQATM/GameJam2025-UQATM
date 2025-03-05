using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnDistance = 2f;
    [SerializeField] private float spawnInterval = 1f;

    [Header("Waypoints")]
    [SerializeField] private Transform leftWaypoint;
    [SerializeField] private Transform rightWaypoint;
    [SerializeField] private Transform finalTarget;

    private bool spawnLeft = true;

    public void SpawnWave(int enemyCount, int enemyHealth)
    {
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

        // Configure enemy health and link to wave system
        EnemyHealth enemyHealthScript = newEnemy.GetComponent<EnemyHealth>();
        if (enemyHealthScript != null)
        {
            enemyHealthScript.SetHealth(hp);
            enemyHealthScript.SetWaveSystem(FindObjectOfType<Waves>());
        }

        // Configure pathfinding
        EnemiesPathFinding enemyPathScript = newEnemy.GetComponent<EnemiesPathFinding>();
        if (enemyPathScript != null)
        {
            enemyPathScript.midWaypoint = spawnLeft ? leftWaypoint : rightWaypoint;
            enemyPathScript.finalTarget = finalTarget;
            spawnLeft = !spawnLeft;
        }
    }
}