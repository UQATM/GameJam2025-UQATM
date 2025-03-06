using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] Image healthBar;

    float hpPercentage;

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

        hpPercentage = (float)pointsDeVie / (float)pvMax;
        Debug.Log(pointsDeVie + " " + pvMax + " " + hpPercentage);
        Debug.Log(pointsDeVie + "/" + pvMax + "=" + pointsDeVie/pvMax);
    }

    public void AddPtsVie(int _nbrPV)
    {
        pointsDeVie += _nbrPV;
        if (pointsDeVie > pvMax)
        {
            pointsDeVie = pvMax;
        }
        hpPercentage = (float)pointsDeVie / (float)pvMax;
        Debug.Log(pointsDeVie + " " + pvMax + " " + hpPercentage);
        Debug.Log(pointsDeVie + "/" + pvMax + "=" + pointsDeVie/pvMax);
        healthBar.fillAmount = hpPercentage;
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
        hpPercentage = (float)pointsDeVie / (float)pvMax;
        Debug.Log(pointsDeVie + " " + pvMax + " " + hpPercentage);
        Debug.Log(pointsDeVie + "/" + pvMax + "=" + pointsDeVie/pvMax);
        healthBar.fillAmount = hpPercentage;
    }
}
