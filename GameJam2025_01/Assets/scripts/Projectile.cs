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
    private float radius;
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

    void HitTarget()
    {
        switch (currentProjectile)
        {
            case projectileType.smallProjectile:
                FireSmallProjectile();
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
        Destroy(gameObject);
    }

    private void FireRocket()
    {
        float blastRadius = 10f; // Adjust as needed
        Vector3 explosionPosition = transform.position;

        // Find all colliders in the explosion radius
        Collider[] hitColliders = Physics.OverlapSphere(explosionPosition, blastRadius, layerMask);

        if (hitColliders.Length > 0)
        {

            foreach (Collider hit in hitColliders)
            {
                //enemie lose hp logic
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

    private void FireSmallProjectile()
    {
        //enemie lose hp logic
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemie")
        {
            HitTarget();
        }
    }
}
