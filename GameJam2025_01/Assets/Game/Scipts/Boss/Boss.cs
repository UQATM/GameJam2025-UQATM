using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int BoosHealth; // Maximum boss health
    public int currentBoosHealth;           // Current boss health
    [SerializeField] private int attackDamage = 50;

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
        Debug.Log(gameObject.name + " health set to: " + hp);
    }

    public void TakeDamage(int damage)
    {
        currentBoosHealth -= damage;
        Debug.Log("Boss took " + damage + " damage! Remaining health: " + currentBoosHealth);

        // Die if health reaches 0 or below (or add any other condition if desired)
        if (currentBoosHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Boss defeated!");
        // Do not call waveSystem.OnEnemyKilled() here so that the boss isn't counted in the wave enemy count.
        Destroy(gameObject);
    }

    public void SetWaveSystem(Waves ws)
    {
        waveSystem = ws;
    }

    // Call this from attack animations or trigger events
    public void PerformAttack()
    {
        Debug.Log("Boss attacks for " + attackDamage + " damage!");
    }
}
