using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
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
    public int price;

    [Header("Where it fire from and speed")]
    public Transform firePoint; // The point from where the projectile will be fired
    public float fireRate = 1f; // Time between shots
    public int damage;
    public float slowTime = 3f;

    [Header("Slow turret setting field of fire")]
    public float viewRadius = 10f;
    public float viewAngle = 90f;

    public float fireCountdown = 0f;

    
    public LayerMask layerMask;

    void Update()
    {
        if (target == null)
        {
            target = findTarget();
        }
        if (target == null)
        {
            return;
        }
        if (Vector3.Distance(transform.position, target.position) > viewRadius)
        {
            target = findTarget();
        }
        if (target == null)
        {
            return;
        }
        
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
                    fireCountdown = 1f / fireRate;
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
        Vector3 explosionPosition = transform.position;

        // Find all colliders in the explosion radius
        Collider[] hitColliders = Physics.OverlapSphere(explosionPosition, viewRadius, layerMask);

        if (hitColliders.Length > 0)
        {
            
            foreach (Collider hit in hitColliders)
            {
                Debug.Log(hit.gameObject.name);
                checkAngle(hit.gameObject);
            }
        }
    }

    void checkAngle(GameObject target)
    {
        Vector3 directionToTarget = (target.transform.position - transform.position).normalized;
        if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
        {
            StartCoroutine(slowingEnemies(target));
        }
    }

    void Shoot()
    {
        GameObject projectileGO = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile projectile = projectileGO.GetComponent<Projectile>();
        switch (currentTurret)
        {
            case turretType.smallTurret:
                projectile.currentProjectile = projectileType.smallProjectile;
                break;
            case turretType.bigTurret:
                projectile.currentProjectile = projectileType.Rocket;
                break;
        }
        if (projectile != null)
        {
            projectile.Seek(target, damage);
        }
    }

    Transform findTarget()
    {
        Vector3 explosionPosition = transform.position;
        
        Collider[] hitColliders = Physics.OverlapSphere(explosionPosition, viewRadius, layerMask);
        Transform closestTarget = null;
        float closestDistance = float.MaxValue;

        if (hitColliders.Length > 0)
        {

            foreach (Collider hit in hitColliders)
            {
                float distance = Vector3.Distance(explosionPosition, hit.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = hit.gameObject.transform;
                }
            }
            if (closestTarget != null)
            {
                return closestTarget.transform;
            }
        }
        else
        {
            return null;
        }
        return null;
    }

    IEnumerator slowingEnemies(GameObject target)
    {
        Debug.Log("enumarator starting");
        float originalSpeed = target.GetComponent<NavMeshAgent>().speed;
        target.GetComponent<NavMeshAgent>().speed = 3;

        yield return new WaitForSeconds(slowTime);
        target.GetComponent<NavMeshAgent>().speed = originalSpeed;
        yield return null;
    }
    private void OnDrawGizmos()
    {
        // Set the color of the Gizmo
        Gizmos.color = Color.red;

        // Draw a wire sphere at the transform's position with the given blast radius
        Gizmos.DrawWireSphere(transform.position, 50f);
    }

}
