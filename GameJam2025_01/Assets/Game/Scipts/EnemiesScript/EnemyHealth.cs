using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int enemyHealth;        
    public int currentEnemyHealth; 
    private Waves waveSystem;

    public void SetHealth(int hp)
    {
        enemyHealth = hp;
        currentEnemyHealth = hp;
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
        if (waveSystem != null)
            waveSystem.OnEnemyKilled();
        Destroy(gameObject);
    }
}
