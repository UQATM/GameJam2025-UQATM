using UnityEngine;

public enum TypeObstacle
{
    Bois,
    Pierre,
    Metal,
}

public class Obstacle : MonoBehaviour
{
    [SerializeField] private TypeObstacle typeObstacle;
    [SerializeField] int pointsDeVie;
    [SerializeField] int pvMax;

    private void Start()
    {
        switch (typeObstacle)
        {
            case TypeObstacle.Bois:
                pointsDeVie = 25;
                pvMax = 25;
                break;
            case TypeObstacle.Pierre:
                pointsDeVie = 10;
                pvMax = 10;
                break;
            case TypeObstacle.Metal:
                pointsDeVie = 20;
                pvMax = 10;
                break;
            default:
                pointsDeVie = 10;
                pvMax = 10;
                break;
        }
    }

    public void AddPtsVie(int _nbrPV)
    {
        pointsDeVie += _nbrPV;
        if (pointsDeVie > pvMax)
        {
            pointsDeVie = pvMax;
        }
        Debug.Log(_nbrPV + " pv ont été ajouté");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                Debug.Log("Obstacle collided with Enemy, dealing 200 damage.");
                enemy.TakeDamage(200);
            }
            else
            {
                Debug.Log("Obstacle collided with Enemy that has no EnemyHealth. Destroying object.");
                Destroy(collision.gameObject);
            }

            pointsDeVie--;
            Debug.Log("Obstacle lost 1 HP from Enemy collision. Remaining HP: " + pointsDeVie);
            if (pointsDeVie <= 0)
            {
                Debug.Log("Obstacle destroyed after Enemy collision.");
                Destroy(gameObject);
            }
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            Boss boss = collision.gameObject.GetComponent<Boss>();
            if (boss != null)
            {
                Debug.Log("Obstacle collided with Boss, dealing 20 damage to Boss.");
                boss.TakeDamage(20);
            }
            else
            {
                Debug.Log("Obstacle collided with Boss that has no Boss script. Destroying object.");
                Destroy(collision.gameObject);
            }

            Debug.Log("Boss inflicted 200 damage to the obstacle.");
            pointsDeVie -= 200;
            if (pointsDeVie <= 0)
            {
                Debug.Log("Obstacle destroyed after Boss collision.");
                Destroy(gameObject);
            }
        }
    }
}
