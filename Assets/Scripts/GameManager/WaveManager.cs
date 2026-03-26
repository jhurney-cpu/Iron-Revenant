using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    [Header("Wave Settings")]
    public int currentWave = 0;
    public int baseZombiesPerWave = 5;
    public float waveMultiplier = 1.3f;
    public float spawnDelay = 1f;

    [Header("Intermission")]
    public float timeBetweenWaves = 5f;
    private bool isIntermission = false;
    private float intermissionTimer = 0f;

    [Header("Zones")]
    public SpawnerZone[] zones;

    [Header("UI")]
    public TextMeshProUGUI waveText;

    private int zombiesLeftToSpawn;
    private int zombiesAlive;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartNextWave();
    }

    private void Update()
    {
        if (isIntermission)
        {
            intermissionTimer -= Time.deltaTime;

            if (waveText != null)
                waveText.text = "Next Wave In: " + Mathf.Ceil(intermissionTimer);

            if (intermissionTimer <= 0)
            {
                isIntermission = false;
                StartNextWave();
            }
        }
    }

    private void StartNextWave()
    {
        currentWave++;

        zombiesLeftToSpawn = Mathf.RoundToInt(baseZombiesPerWave * Mathf.Pow(waveMultiplier, currentWave - 1));
        zombiesAlive = 0;

        if (waveText != null)
            waveText.text = " " + currentWave;

        InvokeRepeating(nameof(SpawnZombie), 0f, spawnDelay);
    }

    private void SpawnZombie()
    {
        if (zombiesLeftToSpawn <= 0)
        {
            CancelInvoke(nameof(SpawnZombie));
            return;
        }

        EnemySpawner spawner = GetActiveSpawner();

        if (spawner != null)
        {
            spawner.SpawnEnemy();
            zombiesAlive++;
            zombiesLeftToSpawn--;
        }
    }

    private EnemySpawner GetActiveSpawner()
    {
        List<SpawnerZone> activeZones = new List<SpawnerZone>();

        foreach (var zone in zones)
        {
            if (zone.isActive)
                activeZones.Add(zone);
        }

        if (activeZones.Count == 0)
            return null;

        SpawnerZone zoneToUse = activeZones[Random.Range(0, activeZones.Count)];
        return zoneToUse.GetRandomSpawner();
    }

    public void ZombieDied()
    {
        zombiesAlive = Mathf.Max(0, zombiesAlive - 1);

        if (zombiesAlive == 0 && zombiesLeftToSpawn == 0)
        {
            isIntermission = true;
            intermissionTimer = timeBetweenWaves;
        }
    }
}