using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum turretType
{
    smallTurret,
    bigTurret,
    slowTurret
}
public class turretScript : MonoBehaviour
{

    [Header("Turret attribute")]
    public turretType currentTurret;
    public Transform target; // The target the turret will aim at
    public float rotationSpeed = 5f; // Speed at which the turret rotates
    public GameObject projectilePrefab; // The projectile the turret will shoot

    [Header("Where it fire from and speed")]
    public Transform firePoint; // The point from where the projectile will be fired
    public float fireRate = 1f; // Time between shots

    [Header("Slow turret setting field of fire")]
    public float viewRadius = 10f;
    public float viewAngle = 90f;

    private float fireCountdown = 0f;

    
    public LayerMask layerMask;

    void Update()
    {
        if (target == null)
            return;

        // Rotate turret to face the target
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        Vector3 directionToTarget = (target.transform.position - transform.position).normalized;
        if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
        {
            if (fireCountdown <= 0f)
            {
                if (currentTurret == turretType.slowTurret)
                {
                    shootingSlow();
                }
                else
                {        
                    Shoot();
                    fireCountdown = 1f / fireRate;
                }
            }
        }
        

        fireCountdown -= Time.deltaTime;
    }

    private void shootingSlow()
    {
        float blastRadius = 10f; // Adjust as needed
        Vector3 explosionPosition = transform.position;

        // Find all colliders in the explosion radius
        Collider[] hitColliders = Physics.OverlapSphere(explosionPosition, blastRadius, layerMask);

        if (hitColliders.Length > 0)
        {
            Debug.Log("Explosion hit " + hitColliders.Length + " targets.");

            foreach (Collider hit in hitColliders)
            {
                Debug.Log("Hit: " + hit.name);
                checkAngle(hit.gameObject);
            }
        }
    }

    void checkAngle(GameObject target)
    {
        Vector3 directionToTarget = (target.transform.position - transform.position).normalized;
        if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
        {
            //implement logic with target to reduce speed
        }
    }

    void Shoot()
    {
        GameObject projectileGO = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile projectile = projectileGO.GetComponent<Projectile>();
        projectile.currentProjectile = projectileType.Rocket;
        if (projectile != null)
        {
            projectile.Seek(target);
        }
    }

}
