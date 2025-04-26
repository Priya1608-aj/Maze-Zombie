using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [Header("Wave Settings")]
    public int startingZombies = 5;
    public int zombiesPerWave = 3;
    public float timeBetweenWaves = 5f;

    [Header("Spawning")]
    public List<GameObject> zombiePrefabs; // List of different zombie types
    public List<Transform> spawnPoints;

    private int currentWave = 0;

    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        while (true)
        {
            currentWave++;

            int zombiesToSpawn = startingZombies + (zombiesPerWave * (currentWave - 1));
            Debug.Log("Spawning wave " + currentWave + " with " + zombiesToSpawn + " zombies.");

            for (int i = 0; i < zombiesToSpawn; i++)
            {
                SpawnZombie();
                yield return new WaitForSeconds(0.3f);
            }

            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    void SpawnZombie()
    {
        if (spawnPoints.Count == 0 || zombiePrefabs.Count == 0)
        {
            Debug.LogWarning("Missing spawn points or zombie prefabs!");
            return;
        }

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        GameObject zombiePrefab = zombiePrefabs[Random.Range(0, zombiePrefabs.Count)];

        Instantiate(zombiePrefab, spawnPoint.position, Quaternion.identity);
    }
}

