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

    public float spawnDelay = 2f;

    private void Start()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPos = spawnPoint.position + new Vector3(0, yOffset, 0);

        GameObject zombie = Instantiate(enemyPrefab, spawnPos, spawnPoint.rotation);

        EnemyAI ai = zombie.GetComponent<EnemyAI>();
        ai.waypoints = pathWaypoints;

        Invoke(nameof(SpawnEnemy), spawnDelay);
    }
}