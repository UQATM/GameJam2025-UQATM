using UnityEngine;

public class Waves : MonoBehaviour
{
    [SerializeField] private int startingEnemies = 5;                   // Number of enemies in the first wave
    [SerializeField] private EnemyHealth enemyHealth;                   // Reference to an EnemyHealth component (for its currentEnemyHealth value)
    [SerializeField] private EnemySpawner enemySpawner;                 // Reference to the EnemySpawner script
    [SerializeField] private float initialWaveDelay = 5f;                 // Delay before the first wave starts (5 seconds)

    private int currentWave = 1;                                          // Tracks the current wave number
    private int enemiesAlive;                                             // Number of enemies alive in the current wave
    private bool waveEnded = true;                                        // Flag to check if the wave has ended
    private int currentEnemyCount;                                        // Number of enemies to spawn in the current wave
    private int currentEnemyHealth;                                       // Current maximum enemy health value to use for this wave

    private void Start()
    {
        // Get the initial enemy health from the provided EnemyHealth reference
        currentEnemyHealth = enemyHealth.currentEnemyHealth;
        currentEnemyCount = startingEnemies;
        // Start the first wave after the specified delay
        Invoke("StartNextWave", initialWaveDelay);
    }

    public void StartNextWave()
    {
        if (!waveEnded) return; // Prevent starting a new wave if the previous hasn't ended
        waveEnded = false;
        // Calculate the enemy count for this wave
        currentEnemyCount = startingEnemies + (currentWave - 1);
        int enemyHealthToUse = currentEnemyHealth;

        // Every 5th wave, add one more enemy and increase enemy health by 1
        if (currentWave % 5 == 0)
        {
            currentEnemyCount++;
            currentEnemyHealth++;
            enemyHealthToUse = currentEnemyHealth;
        }
        // Set the count of alive enemies for this wave
        enemiesAlive = currentEnemyCount;
        // Spawn the wave with the calculated enemy count and enemy health value
        enemySpawner.SpawnWave(currentEnemyCount, enemyHealthToUse);
        currentWave++;
    }

    public void OnEnemyKilled()
    {
        enemiesAlive--;
        if (enemiesAlive <= 0)
        {
            waveEnded = true;
            // After all enemies in the wave are killed, signal the spawner to start the next wave after a delay
            enemySpawner.OnWaveEnded();
        }
    }

    public bool IsWaveEnded()
    {
        return waveEnded;
    }
}
