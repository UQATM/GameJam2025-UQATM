using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class Waves : MonoBehaviour
{
    [SerializeField] private int startingEnemies = 5; // Enemies PER SPAWNER
    [SerializeField] private EnemyHealth enemyHealth;
    [SerializeField] private List<EnemySpawner> enemySpawners; // All 4 spawners
    [SerializeField] private float initialWaveDelay = 5f;
    [SerializeField] private TextMeshProUGUI waveCounterText;

    private int currentWave = 0;
    private int totalEnemiesAlive; // Total enemies across ALL spawners
    private bool waveEnded = true;
    private int currentEnemyCount; // Enemies PER SPAWNER
    private int currentEnemyHealth;

    private void Start()
    {
        currentEnemyHealth = enemyHealth.currentEnemyHealth;
        Invoke("StartNextWave", initialWaveDelay);
    }

    public void StartNextWave()
    {
        if (!waveEnded) return;

        waveEnded = false;
        currentWave++;

        // Calculate enemies per spawner
        currentEnemyCount = startingEnemies + (currentWave - 1);

        // Every 5 waves, add +1 enemy per spawner and increase health
        if (currentWave % 5 == 0)
        {
            currentEnemyCount++;
            currentEnemyHealth++;
        }

        // Spawn enemies on all spawners
        foreach (EnemySpawner spawner in enemySpawners)
        {
            spawner.SpawnWave(currentEnemyCount, currentEnemyHealth);
        }

        // Track total enemies (enemies per spawner × number of spawners)
        totalEnemiesAlive = currentEnemyCount * enemySpawners.Count;
        UpdateWaveCounter();
    }

    public void OnEnemyKilled()
    {
        totalEnemiesAlive--;

        if (totalEnemiesAlive <= 0)
        {
            waveEnded = true;
            foreach (EnemySpawner spawner in enemySpawners)
            {
                spawner.OnWaveEnded();
            }
        }
    }

    private void UpdateWaveCounter()
    {
        if (waveCounterText != null)
            waveCounterText.text = "Wave: " + currentWave;
    }

    public bool IsWaveEnded() => waveEnded;
}