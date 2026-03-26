/*****************************************************************************
* File Name      : EnemySpawner.cs
* Author         : Noah Hurney
* Creation Date  : February 20, 2026
* Last Updated   : March 26, 2026
* Brief Description : Spawns enemies at a designated spawn point and assigns
*                     their waypoint path for movement.
*****************************************************************************/

using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public Transform[] pathWaypoints;
    public float yOffset = 0.5f;

    /// <summary>
    /// Spawns an enemy at the configured spawn point and assigns its waypoint path.
    /// </summary>
    public void SpawnEnemy()
    {
        Vector3 spawnPos = spawnPoint.position + Vector3.up * yOffset;

        GameObject zombie = Instantiate(enemyPrefab, spawnPos, spawnPoint.rotation);

        zombie.GetComponent<EnemyAI>().waypoints = pathWaypoints;
    }
}