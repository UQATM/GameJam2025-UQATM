using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int BoosHealth; // Maximum boss health
    public int currentBoosHealth;           // Current boss health
    [SerializeField] private int attackDamage = 50;
    private economyBase economy;

    private Waves waveSystem;

    private void Start()
    {
        currentBoosHealth = BoosHealth;
        Debug.Log("Boss spawned! Health: " + currentBoosHealth);
        economy = FindObjectOfType<economyBase>();
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

        // Scale down by 1 unit on each axis, but clamp so no axis goes below 1
        Vector3 currentScale = transform.localScale;
        Vector3 newScale = new Vector3(
            Mathf.Max(currentScale.x - 1f, 1f),
            Mathf.Max(currentScale.y - 1f, 1f),
            Mathf.Max(currentScale.z - 1f, 1f)
        );
        transform.localScale = newScale;
        Debug.Log("Boss new scale: " + transform.localScale);

        // Die if health is 0 or below, or if the boss has shrunk to a scale of 1 (on the x-axis)
        if (currentBoosHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Boss defeated!");
        // Do not call waveSystem.OnEnemyKilled() here so that the boss isn't counted in the wave enemy count.
        if (economy != null)
            economy.OnBossKilled();
        Destroy(gameObject);
    }

    public void SetWaveSystem(Waves ws)
    {
        waveSystem = ws;
    }

    // Call this from attack animations or trigger events
    public void PerformAttack()
    {
        Debug.Log(gameObject.name + " attacks for " + attackDamage + " damage!");
    }
}
