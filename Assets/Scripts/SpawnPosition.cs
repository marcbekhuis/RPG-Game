using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPosition : MonoBehaviour
{
    [SerializeField] private MapData mapData;

    private Dictionary<Biome, List<Vector2Int>> filteredPositions = new Dictionary<Biome, List<Vector2Int>>();

    public void Setup()
    {
        filteredPositions.Clear();
        foreach (var landscapeTile in mapData.landscapeTiles)
        {
            if (!filteredPositions.ContainsKey(landscapeTile.biome)) filteredPositions.Add(landscapeTile.biome, new List<Vector2Int>());
            filteredPositions[landscapeTile.biome].Add(landscapeTile.position);
        }
    }

    public Vector2Int GetPosition(Biome[] biomes, int borderDistance)
    {
        List<Vector2Int> biomePositions = new List<Vector2Int>();
        foreach (var biome in biomes)
        {
            foreach (var value in filteredPositions[biome])
            {
                if (value.x + 1 < mapData.size.x - borderDistance && value.x + 1 > borderDistance && value.y + 1 < mapData.size.y - borderDistance && value.y + 1 > borderDistance) biomePositions.Add(value);
            }
        }
        return biomePositions[Random.Range(0, biomePositions.Count)];
    }

    public Vector2Int GetPosition(Biome[] biomes, Vector2Int center, int minRange, int maxRange)
    {
        List<Vector2Int> biomePositions = new List<Vector2Int>();
        foreach (var biome in biomes)
        {
            foreach (var value in filteredPositions[biome])
            {
                float distance = Vector2.Distance(value, center);
                if (distance < maxRange && distance > minRange) biomePositions.Add(value);
            }
        }
        if (biomePositions.Count <= 0) return Vector2Int.left;
        return biomePositions[Random.Range(0, biomePositions.Count)];
    }
}
