using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int enemyHealth;        // Max health for this enemy
    public int currentEnemyHealth; // Current health for this enemy
    private Waves waveSystem;

    public void SetHealth(int enemyHealth)
    {
        enemyHealth = enemyHealth;
        currentEnemyHealth = enemyHealth;
    }

    public void SetWaveSystem(Waves ws)
    {
        waveSystem = ws;
    }

    public void TakeDamage(int damage)
    {
        currentEnemyHealth -= damage;
        if (currentEnemyHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        waveSystem.OnEnemyKilled();
        Destroy(gameObject);
    }
}
