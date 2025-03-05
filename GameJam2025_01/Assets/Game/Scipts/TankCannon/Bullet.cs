using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Enemy")
        {
            Destroy(other.gameObject);

            Destroy(this.gameObject);
        }
    }
}
