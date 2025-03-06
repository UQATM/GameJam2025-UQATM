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
        else if (collision.gameObject.CompareTag("Boss"))
        {
            Boss boss = collision.gameObject.GetComponent<Boss>();
            if (boss != null)
            {
                boss.TakeDamage(20);
            }
            else
            {
                Destroy(collision.gameObject);
            }

            pointsDeVie -= 200;
            if (pointsDeVie <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
