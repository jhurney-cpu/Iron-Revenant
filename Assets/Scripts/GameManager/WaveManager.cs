/*****************************************************************************
* File Name      : WaveManager.cs
* Author         : Noah Hurney
* Creation Date  : March 1, 2026
* Last Updated   : March 26, 2026
* Brief Description : Manages wave progression, zombie spawning, intermissions,
*                     and active spawner zone selection.
*****************************************************************************/

using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    public int currentWave = 0;
    public int baseZombiesPerWave = 5;
    public float waveMultiplier = 1.3f;
    public float spawnDelay = 1f;

    public float timeBetweenWaves = 5f;
    private bool isIntermission = false;
    private float intermissionTimer = 0f;

    public SpawnerZone[] zones;
    public TextMeshProUGUI waveText;

    private int zombiesLeftToSpawn;
    private int zombiesAlive;

    /// <summary>
    /// Assigns the singleton instance.
    /// </summary>
    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Begins the first wave when the scene starts.
    /// </summary>
    private void Start()
    {
        StartNextWave();
    }

    /// <summary>
    /// Handles intermission countdown and triggers the next wave.
    /// </summary>
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

    /// <summary>
    /// Starts a new wave, calculates zombie count, and begins spawning.
    /// </summary>
    private void StartNextWave()
    {
        currentWave++;

        zombiesLeftToSpawn = Mathf.RoundToInt(baseZombiesPerWave * Mathf.Pow(waveMultiplier, currentWave - 1));
        zombiesAlive = 0;

        if (waveText != null)
            waveText.text = " " + currentWave;

        InvokeRepeating(nameof(SpawnZombie), 0f, spawnDelay);
    }

    /// <summary>
    /// Spawns a zombie from an active spawner zone.
    /// </summary>
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

    /// <summary>
    /// Selects a random spawner from the currently active zones.
    /// </summary>
    /// <returns>An EnemySpawner or null if none are active.</returns>
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

    /// <summary>
    /// Called when a zombie dies. Handles intermission logic.
    /// </summary>
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