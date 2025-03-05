using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Enemy")
        {
            EnemyHealth enemyHealth = gameObject.GetComponent<EnemyHealth>();
            if (this.transform.tag == "MachineGun")
            {
                enemyHealth.TakeDamage(5);
            }
            else if (this.transform.tag == "Cannon")
            {
                enemyHealth.TakeDamage(15);
            }
            else if (this.transform.tag == "Grenade")
            {
                Debug.Log(other.gameObject);
            }

            Destroy(this.gameObject);
        }
    }
}
