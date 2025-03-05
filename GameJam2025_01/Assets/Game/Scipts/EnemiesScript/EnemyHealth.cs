using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int enemyHealth;        // Max health for this enemy
    public int currentEnemyHealth; // Current health for this enemy
    private Waves waveSystem;

    public void SetHealth(int hp)
    {
        // Correctly assign the provided hp to the enemyHealth and currentEnemyHealth variables
        enemyHealth = hp;
        currentEnemyHealth = hp;
        // Debug.Log("Enemy health set to: " + hp);
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
        // Debug.Log("Enemy died");
        waveSystem.OnEnemyKilled();
        Destroy(gameObject);
    }
}
