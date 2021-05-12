using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private SpawnEntry[] spawnEntries;
    [SerializeField] private SpawnPosition spawnPosition;
    [SerializeField] private DayNightCycle dayNightCycle;
    [SerializeField] private Transform player;

    [System.Serializable]
    public class SpawnEntry 
    {
        [Tooltip("prefab of the enemy to spawn")] public GameObject prefab;
        [Tooltip("Min range from player")][Min(0)] public int minRange = 2;
        [Tooltip("Max range from player")][Min(0)] public int maxRange = 5;
        [Tooltip("Earliest time enemie spawns")] [Range(0, 24)] public int earlyTime = 0;
        [Tooltip("Latest time enemie spawns")][Range(0,24)] public int lateTime = 24;
        [Tooltip("Latest time enemie spawns")][Min(1)] public int maxSpawns = 5;
        [Tooltip("Time between spawns in seconds")] [Min(0)] public float timeBetweenSpawns = 10;
        [Tooltip("Biomes to spawn enemy in")] public Biome[] spawnBiomes;

        [HideInInspector] public int spawns = 0;
        [HideInInspector] public float nextSpawnTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.consoleOpen || GameManager.instance.GameLoading || GameManager.instance.playerDied) return;
        foreach (var spawnEntry in spawnEntries)
        {
            SpawnEnemy(spawnEntry);
        }
    }

    public void SpawnEnemy(SpawnEntry spawnEntry)
    {
        if (Time.time < spawnEntry.nextSpawnTime) return;
        if (spawnEntry.spawns >= spawnEntry.maxSpawns) return;
        if (dayNightCycle.timeOfDay < spawnEntry.earlyTime && dayNightCycle.timeOfDay > spawnEntry.lateTime) return;
        Vector3 position = (Vector3Int)spawnPosition.GetPosition(spawnEntry.spawnBiomes, Vector2Int.RoundToInt((Vector2)player.position), spawnEntry.minRange, spawnEntry.maxRange);
        if (position.x < 0) return;
        Instantiate(spawnEntry.prefab, position + new Vector3(.5f,.5f,0), new Quaternion(0, 0, 0, 0));
        spawnEntry.spawns++;
        spawnEntry.nextSpawnTime = Time.time + spawnEntry.timeBetweenSpawns;
    }
}
