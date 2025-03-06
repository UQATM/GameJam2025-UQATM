using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum projectileType
{
    smallProjectile,
    ExplosiveProjectile,
    BigProjectile,
    Rocket
}
public class Projectile : MonoBehaviour
{
    public float speed = 10f;

    [Header("Enemie settings")]
    private Transform target;

    [Header("Blast sphere")]
    public float radius;
    public float maxDistance = 10f;
    public int damage;

    [Header("Hit enemie")]
    public LayerMask layerMask;

    public projectileType currentProjectile;

    public void Seek(Transform _target, int damageSet)
    {
        target = _target;
        damage = damageSet;
    }
    private void Start()
    {
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);

        float singleStep = speed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, singleStep, 0.0f);

        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    void HitTarget(GameObject enemy)
    {
        switch (currentProjectile)
        {
            case projectileType.smallProjectile:
                FireSmallProjectile(enemy);
                break;
            case projectileType.ExplosiveProjectile:
                FireExplosiveProjectile();
                break;
            case projectileType.BigProjectile:
                FireBigProjectile();
                break;
            case projectileType.Rocket:
                FireRocket();
                break;
            default:
                Debug.LogWarning("Unknown projectile type!");
                break;
        }
        Debug.Log("should destroy");
        Destroy(gameObject);
    }

    private void FireRocket()
    {
        Debug.Log("rocket blast");
        float blastRadius = radius; // Adjust as needed
        Vector3 explosionPosition = transform.position;

        // Find all colliders in the explosion radius
        Collider[] hitColliders = Physics.OverlapSphere(explosionPosition, blastRadius, layerMask);

        if (hitColliders.Length > 0)
        {

            foreach (Collider hit in hitColliders)
            {
                Debug.Log(hit.gameObject.name);
                hit.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
            }
        }
        else
        {
            Debug.Log("No targets in blast radius.");
        }

    }

    private void FireBigProjectile()
    {
        throw new NotImplementedException();
    }

    private void FireExplosiveProjectile()
    {
        throw new NotImplementedException();
    }

    private void FireSmallProjectile(GameObject enemy)
    {
        Debug.Log("meow");
        enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
        //enemie lose hp logic
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision projectile");
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Ennemy hit");
            HitTarget(collision.gameObject);
        }
    }
}
