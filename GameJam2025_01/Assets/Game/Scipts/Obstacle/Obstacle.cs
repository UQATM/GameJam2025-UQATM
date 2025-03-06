using UnityEngine;
public enum TypeObstacle
{
    Bois,
    Pierre,
    Metal,
}
public class Obstacle : MonoBehaviour
{
    // Sélection du type d’obstacle dans l’Inspector
    [SerializeField] private TypeObstacle typeObstacle;

    // Variable interne pour stocker les points de vie de l’obstacle
    [SerializeField] public int pointsDeVie;

    private void Start()
    {
        // Selon le type d’obstacle, on attribue un HP différent
        switch (typeObstacle)
        {
            case TypeObstacle.Bois:
                pointsDeVie = 25;
                break;
            case TypeObstacle.Pierre:
                pointsDeVie = 10;
                break;
            case TypeObstacle.Metal:
                pointsDeVie = 20;
                break;
            default:
                // Valeur par défaut si aucun case ne correspond
                pointsDeVie = 10;
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(200);
            }
            else
            {
                Destroy(collision.gameObject);
            }

            pointsDeVie--;
            if (pointsDeVie <= 0)
            {
                Destroy(gameObject);
            }
        }
    }


}
