using UnityEngine;
using UnityEngine.UI;

public class BaseHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 10;  // Set this in the Inspector.
    [SerializeField] private int currentHealth;

    [Header("UI Elements")]
    [SerializeField] private Image healthBarFill; // Assign your health bar fill Image here.

    [Header("Damage")]
    [SerializeField] private int damage;
    // Gradient created in code.
    private Gradient healthGradient;

    void Start()
    {
        currentHealth = maxHealth;
        CreateHealthGradient();
        UpdateHealthBar();
    }

    void CreateHealthGradient()
    {
        healthGradient = new Gradient();

        GradientColorKey[] colorKeys = new GradientColorKey[3];
        colorKeys[0] = new GradientColorKey(Color.red, 0.0f);    // Critical health.
        colorKeys[1] = new GradientColorKey(Color.yellow, 0.5f); // Medium health.
        colorKeys[2] = new GradientColorKey(Color.green, 1.0f);  // Full health.

        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0] = new GradientAlphaKey(1.0f, 0.0f);
        alphaKeys[1] = new GradientAlphaKey(1.0f, 1.0f);

        healthGradient.SetKeys(colorKeys, alphaKeys);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();

        // This condition might be adjusted depending on your game logic.
        if (currentHealth <= damage)
        {
            Quit GameManager = gameObject.GetComponent<Quit>();
            GameManager.GameOver();
        }
    }

    void UpdateHealthBar()
    {
        float healthPercent = (float)currentHealth / maxHealth;
        healthBarFill.fillAmount = healthPercent;
        healthBarFill.color = healthGradient.Evaluate(healthPercent);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Instead of instantly destroying the enemy, deal 200 damage to it.
            EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                Debug.Log("Enemy collided with base. Dealing 200 damage to enemy.");
                enemy.TakeDamage(200);
            }
            else
            {
                Debug.Log("Enemy collided with base but no EnemyHealth found. Destroying enemy.");
                Destroy(collision.gameObject);
            }

            Debug.Log("Base takes 1 damage from enemy collision.");
            TakeDamage(1);
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            // When a Boss collides: The Boss takes 200 damage, and the Base takes 50 damage.
            Boss boss = collision.gameObject.GetComponent<Boss>();
            if (boss != null)
            {
                Debug.Log("Boss collided with base. Dealing 200 damage to boss.");
                boss.TakeDamage(200);
            }
            else
            {
                Debug.Log("Boss collided with base but no Boss component found. Destroying boss.");
                Destroy(collision.gameObject);
            }
            Debug.Log("Base takes 50 damage from boss collision.");
            TakeDamage(50);
        }
    }
}
