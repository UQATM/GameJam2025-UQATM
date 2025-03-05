using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnDistance = 2f;
    [SerializeField] private float spawnInterval = 1f;

    [Header("Path Configuration")]
    // Initial splits
    [SerializeField] private Transform split1_Left;
    [SerializeField] private Transform split1_Right;
    [SerializeField] private Transform split2_Left;
    [SerializeField] private Transform split2_Right;

    // Roundabouts
    [SerializeField] private Transform round1;
    [SerializeField] private Transform round2;
    [SerializeField] private Transform round3;
    [SerializeField] private Transform round4;

    // Additional splits
    [SerializeField] private Transform split3_Left;
    [SerializeField] private Transform split3_Right;
    [SerializeField] private Transform split4_Left;
    [SerializeField] private Transform split4_Right;

    [SerializeField] private Transform finalTarget;

    private bool[] splitToggles = new bool[4]; // Tracks left/right for 4 splits

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

        EnemyHealth enemyHealthScript = newEnemy.GetComponent<EnemyHealth>();
        if (enemyHealthScript != null)
        {
            enemyHealthScript.SetHealth(hp);
            enemyHealthScript.SetWaveSystem(FindObjectOfType<Waves>());
        }

        EnemiesPathFinding enemyPathScript = newEnemy.GetComponent<EnemiesPathFinding>();
        if (enemyPathScript != null)
        {
            List<Transform> path = new List<Transform>();

            // First two splits
            path.Add(GetSplitWaypoint(0));
            path.Add(GetSplitWaypoint(1));

            // Roundabouts
            path.Add(round1);
            path.Add(round2);
            path.Add(round3);
            path.Add(round4);

            // Last two splits
            path.Add(GetSplitWaypoint(2));
            path.Add(GetSplitWaypoint(3));

            path.Add(finalTarget);

            enemyPathScript.SetPath(path);

            // Alternate toggles for next enemy
            for (int i = 0; i < splitToggles.Length; i++)
                splitToggles[i] = !splitToggles[i];
        }
    }

    private Transform GetSplitWaypoint(int splitIndex)
    {
        return splitIndex switch
        {
            0 => splitToggles[0] ? split1_Left : split1_Right,
            1 => splitToggles[1] ? split2_Left : split2_Right,
            2 => splitToggles[2] ? split3_Left : split3_Right,
            3 => splitToggles[3] ? split4_Left : split4_Right,
            _ => finalTarget
        };
    }
}