using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private Biome[] spawnBiomes;
    [SerializeField] private int borderDistance = 10;
    [SerializeField] private Transform player;
    [SerializeField] private SpawnPosition spawnPosition;

    public void Spawn()
    {
        player.position = (Vector3Int)spawnPosition.GetPosition(spawnBiomes, borderDistance) + new Vector3(0.5f, 0.5f, 0);
    }
}
