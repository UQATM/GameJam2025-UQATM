using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private int _gunDamage = 1;

    [SerializeField]
    private int _grenadeDamage = 2;

    [SerializeField]
    private int _cannonDamage = 3;

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Enemy")
        {
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            switch (this.transform.tag)
            {
                case "MachineGun":
                    {
                        enemyHealth.TakeDamage(_gunDamage);
                        break;
                    }
                case "Cannon":
                    {
                        enemyHealth.TakeDamage(_cannonDamage);
                        break;
                    }
                case "Grenade":
                    {
                        enemyHealth.TakeDamage(_grenadeDamage);
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
            Destroy(this.gameObject);
        }
        else if (other.transform.tag == "Boss")
        {
            Boss boss = other.gameObject.GetComponent<Boss>();
            switch (this.transform.tag)
            {
                case "MachineGun":
                    {
                        boss.TakeDamage(_gunDamage);
                        break;
                    }
                case "Cannon":
                    {
                        boss.TakeDamage(_cannonDamage);
                        break;
                    }
                case "Grenade":
                    {
                        boss.TakeDamage(_grenadeDamage);
                        break;
                    }

                default:
                    break;
            }
            Destroy(this.gameObject);
        }
    }
}
