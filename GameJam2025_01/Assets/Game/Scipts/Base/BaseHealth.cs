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
            // Instead of instantly destroying the enemy, deal 1 damage to it.
            EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(200);
            }
            else
            {
                Destroy(collision.gameObject);
            }

            // Also deal 1 damage to the base.
            TakeDamage(1);
        }
    }

}
