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
        Debug.Log(gameObject.name + " health set to: " + hp);
    }

    public void SetWaveSystem(Waves ws)
    {
        waveSystem = ws;
        Debug.Log(gameObject.name + " wave system set.");
    }

    public void TakeDamage(int damage)
    {
        currentEnemyHealth -= damage;
        Debug.Log(gameObject.name + " took " + damage + " damage, current health: " + currentEnemyHealth);
        if (currentEnemyHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " died.");
        if (waveSystem != null)
            waveSystem.OnEnemyKilled();
        else
            Debug.LogWarning(gameObject.name + " has no wave system reference!");
        Destroy(gameObject);
    }
}