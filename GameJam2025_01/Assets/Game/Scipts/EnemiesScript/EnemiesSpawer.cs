using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnDistance = 2f;
    [SerializeField] private float spawnInterval = 1f;

    [Header("Path Options")]
    [SerializeField] private bool useRoundaboutPath = false;
    [SerializeField] private Transform leftWaypoint;
    [SerializeField] private Transform rightWaypoint;
    [SerializeField] private Transform round1Waypoint;
    [SerializeField] private Transform round2Waypoint;
    [SerializeField] private Transform finalTarget;

    private bool spawnLeft = true;

    public void SpawnWave(int enemyCount, int enemyHealth)
    {
        StartCoroutine(SpawnWaveCoroutine(enemyCount, enemyHealth));
    }

    private IEnumerator SpawnWaveCoroutine(int enemyCount, int enemyHealth)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy(enemyHealth);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy(int hp)
    {
        Vector3 spawnPos = transform.position + transform.forward * spawnDistance;
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPos, transform.rotation);

        // Health setup
        EnemyHealth enemyHealthScript = newEnemy.GetComponent<EnemyHealth>();
        if (enemyHealthScript != null)
        {
            enemyHealthScript.SetHealth(hp);
            enemyHealthScript.SetWaveSystem(FindObjectOfType<Waves>());
        }

        // Path configuration
        EnemiesPathFinding enemyPathScript = newEnemy.GetComponent<EnemiesPathFinding>();
        if (enemyPathScript != null)
        {
            if (useRoundaboutPath)
            {
                // Roundabout path sequence
                List<Transform> roundaboutPath = new List<Transform>
                {
                    round1Waypoint,
                    round2Waypoint,
                    finalTarget
                };
                enemyPathScript.SetPath(roundaboutPath);
            }
            else
            {
                // Original left/right alternating path
                List<Transform> alternatePath = new List<Transform>
                {
                    spawnLeft ? leftWaypoint : rightWaypoint,
                    finalTarget
                };
                enemyPathScript.SetPath(alternatePath);
                spawnLeft = !spawnLeft;
            }
        }
    }
}