using UnityEngine;

public class Waves : MonoBehaviour
{
    [SerializeField] private int startingEnemies = 5; // Number of enemies in the first wave
    [SerializeField] private EnemyHealth enemyHealth;   // Reference to an EnemyHealth to get initial health value
    [SerializeField] private EnemySpawner enemySpawner; // Reference to the EnemySpawner
    [SerializeField] private float initialWaveDelay = 5f; // Delay before the first wave starts (5 seconds)

    private int currentWave = 1;       // Current wave number
    private int enemiesAlive;          // Count of enemies alive in the current wave
    private bool waveEnded = true;     // Flag to track if the wave has ended
    private int currentEnemyCount;     // Number of enemies to spawn in the current wave
    private int currentEnemyHealth;    // Current max enemy health to assign to new enemies

    private void Start()
    {
        currentEnemyHealth = enemyHealth.currentEnemyHealth;
        currentEnemyCount = startingEnemies;
        // Debug.Log("Starting Waves. Initial enemy health: " + currentEnemyHealth + ", starting enemies: " + startingEnemies);
        Invoke("StartNextWave", initialWaveDelay);
    }

    public void StartNextWave()
    {
        if (!waveEnded)
        {
            // Debug.Log("StartNextWave() called but the current wave hasn't ended yet.");
            return;
        }
        waveEnded = false;
        currentEnemyCount = startingEnemies + (currentWave - 1);
        int enemyHealthToUse = currentEnemyHealth;

        if (currentWave % 5 == 0)
        {
            currentEnemyCount++;
            currentEnemyHealth++;
            enemyHealthToUse = currentEnemyHealth;
            // Debug.Log("Wave " + currentWave + " bonus: extra enemy and increased enemy health to " + currentEnemyHealth);
        }
        enemiesAlive = currentEnemyCount;
        //Debug.Log("Starting Wave " + currentWave + " with " + currentEnemyCount + " enemies.");
        enemySpawner.SpawnWave(currentEnemyCount, enemyHealthToUse);
        currentWave++;
    }

    public void OnEnemyKilled()
    {
        enemiesAlive--;
        Debug.Log("Enemy killed. Remaining enemies: " + enemiesAlive);
        if (enemiesAlive <= 0)
        {
            waveEnded = true;
            // Debug.Log("Wave ended. All enemies defeated.");
            enemySpawner.OnWaveEnded();
        }
    }

    public bool IsWaveEnded()
    {
        return waveEnded;
    }
}
