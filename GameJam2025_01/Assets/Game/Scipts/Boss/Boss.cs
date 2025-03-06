using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Settings")]
     private int BoosHealth;
     public int currentBoosHealth;
     private int attackDamage;

    private int currentHealth;
    private Waves waveSystem;

    private void Start()
    {
        currentBoosHealth = BoosHealth;
        Debug.Log("Boss spawned! Health: " + currentBoosHealth);
    }

    public void SetHealth(int hp)
    {
        BoosHealth = hp;
        currentBoosHealth = hp;
    }


    public void TakeDamage(int damage)
    {
        currentBoosHealth -= damage;
        Debug.Log("Boss took " + damage + " damage! Remaining health: " + currentBoosHealth);

        if (currentBoosHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Boss defeated!");
        waveSystem.OnEnemyKilled();
        Destroy(gameObject);
    }

    public void SetWaveSystem(Waves ws)
    {
        waveSystem = ws;
    }

    // Call this from attack animations or trigger events
    public void PerformAttack()
    {
        // Implement your attack logic here
        Debug.Log("Boss attacks for " + attackDamage + " damage!");
    }
}