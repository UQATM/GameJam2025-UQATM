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
}