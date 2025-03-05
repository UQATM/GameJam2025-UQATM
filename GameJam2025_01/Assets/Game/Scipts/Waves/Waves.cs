using TMPro;
using UnityEngine;

public class Waves : MonoBehaviour
{
    [SerializeField] private int startingEnemies = 5;
    [SerializeField] private EnemyHealth enemyHealth;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private float initialWaveDelay = 5f;
    [SerializeField] private TextMeshProUGUI waveCounterText;

    private int currentWave = 0; // Start at 0 to correctly handle the first increment
    private int enemiesAlive;
    private bool waveEnded = true;
    private int currentEnemyCount;
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

        currentEnemyCount = startingEnemies + (currentWave - 1);

        int enemyHealthToUse = currentEnemyHealth;
        if (currentWave % 5 == 0)
        {
            currentEnemyCount++;
            currentEnemyHealth++;
            enemyHealthToUse = currentEnemyHealth;
        }

        enemiesAlive = currentEnemyCount;
        Debug.Log($"Starting Wave {currentWave} with {currentEnemyCount} enemies.");
        enemySpawner.SpawnWave(currentEnemyCount, enemyHealthToUse);
        UpdateWaveCounter(); 
    }

    public void OnEnemyKilled()
    {
        enemiesAlive--;
        Debug.Log($"Enemy killed. Remaining: {enemiesAlive}");

        if (enemiesAlive <= 0)
        {
            waveEnded = true;
            enemySpawner.OnWaveEnded();
        }
    }

    private void UpdateWaveCounter()
    {
        if (waveCounterText != null)
            waveCounterText.text = "Wave: " + currentWave;
    }

    public bool IsWaveEnded() => waveEnded;
}