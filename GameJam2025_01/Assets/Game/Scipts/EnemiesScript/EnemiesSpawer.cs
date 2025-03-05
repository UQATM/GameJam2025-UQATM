using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnDistance = 2f;
    [SerializeField] private float spawnInterval = 1f; // Interval between spawning each enemy (if you want this to be 5 seconds, set to 5f)
    [SerializeField] private Waves waveSystem;

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
        yield return new WaitForSeconds(5f); // Wait 5 seconds after the wave ends before starting the next wave
        waveSystem.StartNextWave();
    }
}
