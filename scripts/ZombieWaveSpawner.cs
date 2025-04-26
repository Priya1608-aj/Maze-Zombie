using UnityEngine;
using System.Collections;

public class ZombieWaveSpawner : MonoBehaviour
{
    [Header("Zombie Settings")]
    public GameObject zombiePrefab;
    public int zombiesPerWave = 10;
    public float spawnDelay = 0.5f;
    public float waveDelay = 5f;

    [Header("Maze Settings")]
    public Vector3 spawnAreaCenter;
    public Vector3 spawnAreaSize;
    public LayerMask groundLayer;
    public float zombieSpacing = 1.5f;
    public float groundCheckDistance = 5f;

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (true)
        {
            Debug.Log("Spawning new wave...");
            for (int i = 0; i < zombiesPerWave; i++)
            {
                Vector3 spawnPos = GetValidSpawnPosition();
                if (spawnPos != Vector3.zero)
                {
                    Instantiate(zombiePrefab, spawnPos, Quaternion.identity);
                }
                else
                {
                    Debug.LogWarning("Couldn't find valid spawn position!");
                }

                yield return new WaitForSeconds(spawnDelay);
            }
            yield return new WaitForSeconds(waveDelay);
        }
    }

    private Vector3 GetValidSpawnPosition()
    {
        for (int attempt = 0; attempt < 20; attempt++)
        {
            Vector3 randomPos = spawnAreaCenter + new Vector3(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                10f, // Start ray high above maze
                Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
            );

            // Raycast downward to find ground
            if (Physics.Raycast(randomPos, Vector3.down, out RaycastHit hit, groundCheckDistance, groundLayer))
            {
                Vector3 spawnPoint = hit.point;

                // Check for nearby zombies to prevent overlapping
                if (!Physics.CheckSphere(spawnPoint, zombieSpacing))
                {
                    return spawnPoint + Vector3.up * 0.5f; // Lift a little to avoid sticking into ground
                }
            }
        }
        return Vector3.zero; // Failed to find position
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(spawnAreaCenter, spawnAreaSize);
    }
}
