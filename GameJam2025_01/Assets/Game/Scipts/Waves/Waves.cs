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
    [SerializeField] private int bossHealth = 50;
    [SerializeField] private float bossSpawnDistance;
    [SerializeField] private float bossSpawnDelay;  // Delay before boss spawns each round

    // This static variable holds the current wave number.
    public static int currentWave = 0;

    // totalEnemiesAlive tracks how many regular enemies (plus boss, if counted) are still on the map.
    private int totalEnemiesAlive;
    private int currentEnemiesPerSpawner;
    private int currentEnemyHealth;

    private void Start()
    {
        currentWave = 0;
        currentEnemiesPerSpawner = startingEnemiesPerSpawner;
        currentEnemyHealth = startingEnemyHealth;
        Debug.Log("Starting Waves. Initial wave delay: " + initialWaveDelay);
        Invoke("StartNextWave", initialWaveDelay); // Initial 5-second delay
    }

    public void StartNextWave()
    {
        currentWave++;
        Debug.Log("Wave " + currentWave + " started.");
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
        Debug.Log("Spawning wave " + currentWave + " with a total of " + totalEnemiesAlive + " enemies.");
        foreach (EnemySpawner spawner in enemySpawners)
        {
            spawner.SpawnWave(currentEnemiesPerSpawner, currentEnemyHealth);
        }

        // Spawn the boss every round, after a delay so it appears near the end of the round.
        // The boss IS counted in the totalEnemiesAlive.
        Invoke("SpawnBossAtRandomSpawner", bossSpawnDelay);
    }

    // This method is called by the enemy and boss Die() methods.
    public void OnEnemyKilled()
    {
        totalEnemiesAlive--;
        Debug.Log("OnEnemyKilled called. Total remaining enemies: " + totalEnemiesAlive);
        if (totalEnemiesAlive <= 0)
        {
            Debug.Log("Wave " + currentWave + " ended.");
            Invoke("StartNextWave", waveCooldown); // 5-second cooldown before next wave
        }
    }

    private void UpdateWaveCounter()
    {
        if (waveCounterText != null)
            waveCounterText.text = $"Wave: {currentWave}";
    }

    // --- Method for boss spawning ---
    private void SpawnBossAtRandomSpawner()
    {
        if (enemySpawners.Count == 0 || bossPrefab == null) return;

        // Pick a random spawner from the list.
        int randomIndex = Random.Range(0, enemySpawners.Count);
        enemySpawners[randomIndex].SpawnBoss(bossPrefab, bossHealth, bossSpawnDistance);

        // Boss is now counted in the enemy count so that the wave doesn't finish until the boss dies.
        //totalEnemiesAlive++;
        Debug.Log("Boss spawned at spawner index " + randomIndex + ". Total enemies now: " + totalEnemiesAlive);
    }
}
