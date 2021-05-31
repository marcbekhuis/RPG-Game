using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandscapeTileDetector : MonoBehaviour
{
    public Biome biome;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.GamePaused()) return;
        FindBiome();
    }

    public void FindBiome()
    {
        Vector2Int gridPosition = Vector2Int.RoundToInt((Vector2)transform.position - Vector2.one * 0.5f);
        biome = MapData.instance.landscapeTiles[gridPosition.x, gridPosition.y].biome;
    }
}
