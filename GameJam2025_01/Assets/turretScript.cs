using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretBehaviourScript : MonoBehaviour
{
    public Transform target; // The target the turret will aim at
    public float rotationSpeed = 5f; // Speed at which the turret rotates
    public GameObject projectilePrefab; // The projectile the turret will shoot
    public Transform firePoint; // The point from where the projectile will be fired
    public float fireRate = 1f; // Time between shots

    private float fireCountdown = 0f;

    void Update()
    {
        if (target == null)
            return;

        // Rotate turret to face the target
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        // Shoot at the target
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject projectileGO = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile projectile = projectileGO.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.Seek(target);
        }
    }
}
