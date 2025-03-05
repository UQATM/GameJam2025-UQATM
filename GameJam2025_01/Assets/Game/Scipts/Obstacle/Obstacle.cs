using UnityEngine;
public enum TypeObstacle
{
    Bois,
    Pierre,
    Metal,
}
public class Obstacle : MonoBehaviour
{
    // S�lection du type d�obstacle dans l�Inspector
    [SerializeField] private TypeObstacle typeObstacle;

    // Variable interne pour stocker les points de vie de l�obstacle
    [SerializeField] private int pointsDeVie;

    private void Start()
    {
        // Selon le type d�obstacle, on attribue un HP diff�rent
        switch (typeObstacle)
        {
            case TypeObstacle.Bois:
                pointsDeVie = 5;
                break;
            case TypeObstacle.Pierre:
                pointsDeVie = 10;
                break;
            case TypeObstacle.Metal:
                pointsDeVie = 20;
                break;
            default:
                // Valeur par d�faut si aucun case ne correspond
                pointsDeVie = 10;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // V�rifie si l�objet entrant est un ennemi (tag "Enemy")
        if (other.CompareTag("Enemy"))
        {
            // 1) D�truire l�ennemi
            Destroy(other.gameObject);

            // 2) L�obstacle perd 1 point de vie
            pointsDeVie--;

            // 3) Si l�obstacle n�a plus de points de vie, on le d�truit
            if (pointsDeVie <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
