using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  // prefab of the enemy to be spawned
    public float spawnInterval = 2f;  // interval between enemy spawns
    public float spawnRadius = 5f;  // radius of the spawn area
    public Transform spawnCenter;  // center of the spawn area

    private void Start()
    {
        // start the enemy spawning coroutine
        StartCoroutine(SpawnEnemiesCoroutine());
    }

    private IEnumerator SpawnEnemiesCoroutine()
    {
        while (true)
        {
            // calculate a random angle within a full circle
            float randomAngle = Random.Range(0f, 2f * Mathf.PI);

            // calculate a random point within the spawn area (scary math)
            Vector2 randomPoint = spawnCenter.position + new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)) * spawnRadius;

            // spawn the enemy at the random point
            Instantiate(enemyPrefab, randomPoint, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
