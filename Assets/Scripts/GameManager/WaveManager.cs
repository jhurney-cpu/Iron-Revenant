using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    [Header("Wave Settings")]
    public int currentWave = 0;
    public int baseZombiesPerWave = 5;
    public float waveMultiplier = 1.3f;   // COD-style scaling
    public float spawnDelay = 1f;

    [Header("Runtime")]
    private int zombiesLeftToSpawn;
    private int zombiesAlive;

    [Header("Spawners")]
    public EnemySpawner[] spawners;

    [Header("UI")]
    public TextMeshProUGUI waveText;

    [Header("Intermission")]
    public float timeBetweenWaves = 7f;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartNextWave();
    }

    private void StartNextWave()
    {
        currentWave++;

        // Calculate zombies for this wave
        zombiesLeftToSpawn = Mathf.RoundToInt(baseZombiesPerWave * Mathf.Pow(waveMultiplier, currentWave - 1));
        zombiesAlive = 0;

        // Update UI
        if (waveText != null)
            waveText.text = " " + currentWave;

        // Begin spawning
        InvokeRepeating(nameof(SpawnZombie), 0f, spawnDelay);
    }

    private void SpawnZombie()
    {
        if (zombiesLeftToSpawn <= 0)
        {
            CancelInvoke(nameof(SpawnZombie));
            return;
        }

        // Pick a random spawner
        EnemySpawner spawner = spawners[Random.Range(0, spawners.Length)];

        // Spawn the zombie
        spawner.SpawnEnemy();

        zombiesLeftToSpawn--;
        zombiesAlive++;
    }

    public void ZombieDied()
    {
        zombiesAlive = Mathf.Max(0, zombiesAlive - 1);

        if (zombiesAlive == 0 && zombiesLeftToSpawn == 0)
        {
            // Start intermission
            Invoke(nameof(StartNextWave), timeBetweenWaves);
        }
    }
}