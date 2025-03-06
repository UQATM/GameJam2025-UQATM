using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class Waves : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int startingEnemiesPerSpawner = 5;
    [SerializeField] private int startingEnemyHealth = 1;
    [SerializeField] private List<EnemySpawner> enemySpawners;
    [SerializeField] private float initialWaveDelay = 5f;
    [SerializeField] private float waveCooldown = 5f;
    [SerializeField] public TextMeshProUGUI waveCounterText;

    [Header("Boss Settings")]
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private int bossHealth = 30;
    [SerializeField] private float bossSpawnDistance;
    [SerializeField] private float bossSpawnDelay;  // Delay before boss spawns each round

    private int currentWave = 0;
    private int totalEnemiesAlive;
    private int currentEnemiesPerSpawner;
    private int currentEnemyHealth;

    private void Start()
    {
        currentEnemiesPerSpawner = startingEnemiesPerSpawner;
        currentEnemyHealth = startingEnemyHealth;
        Invoke("StartNextWave", initialWaveDelay); // Initial 5-second delay
    }

    public void StartNextWave()
    {
        currentWave++;
        UpdateWaveCounter();

        // Calculate enemies and health for this wave
        currentEnemiesPerSpawner = startingEnemiesPerSpawner + (currentWave - 1);
        if (currentWave % 5 == 0)
        {
            currentEnemiesPerSpawner++;
            currentEnemyHealth++;
        }

        // Spawn enemies across all spawners
        totalEnemiesAlive = currentEnemiesPerSpawner * enemySpawners.Count;
        foreach (EnemySpawner spawner in enemySpawners)
        {
            spawner.SpawnWave(currentEnemiesPerSpawner, currentEnemyHealth);
        }

        // Spawn the boss every round, but after a delay (so it appears near the end of the round)
        Invoke("SpawnBossAtRandomSpawner", bossSpawnDelay);
    }

    public void OnEnemyKilled()
    {
        totalEnemiesAlive--;
        if (totalEnemiesAlive <= 0)
        {
            Invoke("StartNextWave", waveCooldown); // 5-second cooldown
        }
    }

    private void UpdateWaveCounter()
    {
        if (waveCounterText != null)
            waveCounterText.text = $"Wave: {currentWave}";
    }

    // --- Modified method for boss spawning ---
    private void SpawnBossAtRandomSpawner()
    {
        if (enemySpawners.Count == 0 || bossPrefab == null) return;

        // Pick a random spawner from the list
        int randomIndex = Random.Range(0, enemySpawners.Count);
        enemySpawners[randomIndex].SpawnBoss(bossPrefab, bossHealth, bossSpawnDistance);

        // DO NOT add the boss to totalEnemiesAlive so that the wave ends based solely on regular enemies.
        // totalEnemiesAlive++;
    }
}
