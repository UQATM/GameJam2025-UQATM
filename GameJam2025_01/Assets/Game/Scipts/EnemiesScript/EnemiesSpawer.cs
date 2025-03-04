using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // The enemy prefab to spawn
    public GameObject enemyPrefab;

    // Time interval between spawns in seconds
    public float spawnInterval = 2f;

    // Start is called before the first frame update
    void Start()
    {
        // Begin spawning enemies immediately and repeat every spawnInterval seconds
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    // Method to spawn the enemy prefab
    void SpawnEnemy()
    {
        if (enemyPrefab != null)
        {
            // Instantiate enemy at the spawner's position with no rotation
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Enemy prefab is not assigned in the inspector!");
        }
    }
}
