using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Enemy")
        {
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            switch (this.transform.tag)
            {
                case "MachineGun":
                    {
                        enemyHealth.TakeDamage(5);
                        break;
                    }
                case "Cannon":
                    {
                        enemyHealth.TakeDamage(15);
                        break;
                    }
                case "Grenade":
                    {
                        enemyHealth.TakeDamage(10);
                        break;
                    }

                default:
                    break;
            }
            Destroy(this.gameObject);
        }

        else if (other.transform.tag == "Obstacle")
        {
            Obstacle obstacle = other.gameObject.GetComponent<Obstacle>();
            switch (this.transform.tag)
            {
                case "Grenade":
                    {
                        obstacle.AddPtsVie(10);
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
