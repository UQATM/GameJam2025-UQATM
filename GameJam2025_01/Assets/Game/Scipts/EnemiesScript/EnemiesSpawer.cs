using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;                    // Prefab of the enemy to spawn
    [SerializeField] private float spawnDistance = 2f;                     // Distance from the spawner at which to spawn enemies
    [SerializeField] private float spawnInterval = 1f;                     // Time interval between individual enemy spawns
    [SerializeField] private Waves waveSystem;                           // Reference to the Waves script

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
        EnemyHealth enemyHealthScript = newEnemy.GetComponent<EnemyHealth>();
        if (enemyHealthScript != null)
        {
            enemyHealthScript.SetHealth(hp);
            enemyHealthScript.SetWaveSystem(waveSystem);
        }
    }

    public void OnWaveEnded()
    {
        StartCoroutine(WaveEndedCoroutine());
    }

    private IEnumerator WaveEndedCoroutine()
    {
        // Wait 5 seconds after the wave ends before starting the next wave
        yield return new WaitForSeconds(5f);
        waveSystem.StartNextWave();
    }
}
