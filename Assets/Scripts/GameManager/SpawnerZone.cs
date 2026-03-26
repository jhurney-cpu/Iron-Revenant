/*****************************************************************************
* File Name      : SpawnerZone.cs
* Author         : Noah Hurney
* Creation Date  : February 25, 2026
* Last Updated   : March 26, 2026
* Brief Description : Controls activation of enemy spawners within a zone and
*                     provides a random active spawner when requested.
*****************************************************************************/

using UnityEngine;

public class SpawnerZone : MonoBehaviour
{
    public EnemySpawner[] spawners;
    public bool isActive = false;

    /// <summary>
    /// Returns a random spawner from the zone if it is active.
    /// </summary>
    /// <returns>A random EnemySpawner or null if inactive.</returns>
    public EnemySpawner GetRandomSpawner()
    {
        if (!isActive || spawners.Length == 0)
            return null;

        return spawners[Random.Range(0, spawners.Length)];
    }
}