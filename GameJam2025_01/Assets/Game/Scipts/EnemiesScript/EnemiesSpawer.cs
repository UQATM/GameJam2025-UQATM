using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnDistance = 2f;
    [SerializeField] private float spawnInterval = 1f;

    [Header("Path Configuration")]
    [SerializeField] private Transform finalTarget;

    [Header("References")]
    [SerializeField] private Waves waveSystem;

    public void SpawnWave(int enemyCount, int enemyHealth)
    {
        StartCoroutine(SpawnWaveCoroutine(enemyCount, enemyHealth));
    }

    private IEnumerator SpawnWaveCoroutine(int enemyCount, int enemyHealth)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPos = transform.position + transform.forward * spawnDistance;
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPos, transform.rotation);

            // Configure health and wave system
            EnemyHealth healthScript = newEnemy.GetComponent<EnemyHealth>();
            healthScript.SetHealth(enemyHealth);
            healthScript.SetWaveSystem(waveSystem);

            // Configure direct pathfinding to final target
            EnemiesPathFinding pathScript = newEnemy.GetComponent<EnemiesPathFinding>();
            if (pathScript != null && finalTarget != null)
            {
                pathScript.SetFinalTarget(finalTarget);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

}