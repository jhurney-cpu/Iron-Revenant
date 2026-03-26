using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject enemyPrefab;
    public Transform spawnPoint;

    [Header("Path Settings")]
    public Transform[] pathWaypoints;

    [Header("Spawn Offset")]
    public float yOffset = 0.5f;   // Adjust in Inspector

    public void SpawnEnemy()
    {
        Vector3 spawnPos = spawnPoint.position + Vector3.up * yOffset;

        GameObject zombie = Instantiate(enemyPrefab, spawnPos, spawnPoint.rotation);

        zombie.GetComponent<EnemyAI>().waypoints = pathWaypoints;
    }
}