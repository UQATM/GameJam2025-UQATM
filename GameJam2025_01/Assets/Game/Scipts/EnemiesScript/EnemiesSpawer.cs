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
    public void SpawnBoss(GameObject bossPrefab, int bossHealth, float bossSpawnDistance)
    {
        Vector3 spawnPos = transform.position + transform.forward * bossSpawnDistance;
        GameObject boss = Instantiate(bossPrefab, spawnPos, transform.rotation);

        // Configure the boss's wave system reference without overriding its health.
        Boss bossScript = boss.GetComponent<Boss>();
        if (bossScript != null)
        {
            // Optionally, if you want to override the boss's health from the wave script, uncomment the following line.
            bossScript.SetHealth(bossHealth);
            bossScript.SetWaveSystem(waveSystem);
        }

        // Configure pathfinding for the boss using the same final target as regular enemies.
        EnemiesPathFinding pathScript = boss.GetComponent<EnemiesPathFinding>();
        if (pathScript != null && finalTarget != null)
        {
            pathScript.SetFinalTarget(finalTarget);
        }
    }

}