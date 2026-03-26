using UnityEngine;

public class SpawnerZone : MonoBehaviour
{
    public EnemySpawner[] spawners;
    public bool isActive = false;

    public EnemySpawner GetRandomSpawner()
    {
        if (!isActive || spawners.Length == 0)
            return null;

        return spawners[Random.Range(0, spawners.Length)];
    }
}