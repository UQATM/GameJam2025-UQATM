using UnityEngine;

public class Waves : MonoBehaviour
{
    [SerializeField] private int startingEnemies = 5;
    [SerializeField] private EnemyHealth enemyHealth;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private float initialWaveDelay = 5f; // Delay before the first wave starts (5 seconds)

    private int currentWave = 1;
    private int enemiesAlive;
    private bool waveEnded = true;
    private int currentEnemyCount;
    private int currentEnemyHealth;

    private void Start()
    {
        currentEnemyHealth = enemyHealth.currentEnemyHealth; // Get initial enemy health from EnemyHealth script
        currentEnemyCount = startingEnemies;
        Invoke("StartNextWave", initialWaveDelay); // Start first wave after 5 seconds
    }

    public void StartNextWave()
    {
        if (!waveEnded) return;
        waveEnded = false;
        currentEnemyCount = startingEnemies + (currentWave - 1);
        int enemyHealthToUse = currentEnemyHealth;
        if (currentWave % 5 == 0)
        {
            currentEnemyCount++;
            currentEnemyHealth++;
            enemyHealthToUse = currentEnemyHealth;
        }
        enemiesAlive = currentEnemyCount;
        enemySpawner.SpawnWave(currentEnemyCount, enemyHealthToUse);
        currentWave++;
    }

    public void OnEnemyKilled()
    {
        enemiesAlive--;
        if (enemiesAlive <= 0)
        {
            waveEnded = true;
            enemySpawner.OnWaveEnded();
        }
    }

    public bool IsWaveEnded()
    {
        return waveEnded;
    }
}
