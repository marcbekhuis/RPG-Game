using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandscapeTileDetector : MonoBehaviour
{
    public Biome FindBiome()
    {
        if (!MapData.instance.gameObject.activeSelf) return null;
        Vector2Int gridPosition = Vector2Int.RoundToInt((Vector2)transform.position - Vector2.one * 0.5f);
        return MapData.instance.landscapeTiles[gridPosition.x, gridPosition.y].biome;
    }
}
