using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(MapData))]
public class LandscapeGenerator : MonoBehaviour
{
    [SerializeField] private float maxHeight = 100;
    [SerializeField] private float seaLevel = 40;
    [SerializeField] private float minTempature = -50;
    [SerializeField] private float maxTempature = 50;
    [SerializeField][Min(1)] private int wallWidth = 1;
    [Space]
    [SerializeField] private Tilemap landscapeTilemap;
    [SerializeField] private Biome[] biomes;
    [SerializeField] private Biome wallBiome;
    [SerializeField] private Biome errorBiome;
    [Space]
    [SerializeField] private HeightNoise heightNoise = new HeightNoise();
    [SerializeField] private WetnessNoise wetnessNoise = new WetnessNoise();
    [SerializeField] private TempatureNoise tempatureNoise = new TempatureNoise();

    private MapData mapData;

    public IEnumerator MakeLandscape(Vector2Int mapSize)
    {
        mapData = GetComponent<MapData>();

        if (biomes.Length <= 0)
        {
            GameGenerator.updateLoadText.Invoke("No biomes in biome list");
            Debug.LogError("No biomes in biome list");
            yield break;
        }
        yield return StartCoroutine(GenerateLandscape(mapSize));
        yield return StartCoroutine(GenerateBorderWall(mapSize));
        yield return StartCoroutine(PlaceLandscape(mapSize));
    }

    public IEnumerator GenerateLandscape(Vector2Int mapSize)
    {
        mapData.landscapeTiles = new LandscapeTile[mapSize.x, mapSize.y];
        mapData.size = mapSize;
        heightNoise.Setup();
        wetnessNoise.Setup();
        tempatureNoise.Setup();

        GameGenerator.updateLoadText.Invoke("Starting landscape Generation");
        System.DateTime loopStartTime = System.DateTime.Now;
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                GenerateTile(x, y);
                if ((System.DateTime.Now - loopStartTime).TotalSeconds >= 1)
                {
                    GameGenerator.updateLoadText.Invoke("Progress landscape generation at tile: X=" + x + " Y=" + y);
                    yield return null;
                    loopStartTime = System.DateTime.Now;
                }
            }
        }
    }

    public void GenerateTile(int x, int y)
    {
        float height = heightNoise.GetValue(x, y, maxHeight);
        float wetness = wetnessNoise.GetValue(x, y);
        float tempature = tempatureNoise.GetValue(x, y, minTempature, maxTempature);
        float waterLevel = seaLevel - height;
        List<Biome> AllowedBiomes = new List<Biome>();

        foreach (var biome in biomes)
        {
            if(height >= biome.minLandHeight && height <= biome.maxLandHeight)
            {
                if(wetness >= biome.minWetness && wetness <= biome.maxWetness)
                {
                    if(tempature >= biome.minTempature && tempature <= biome.maxTempature)
                    {
                        if (waterLevel >= biome.minWaterHeight && waterLevel <= biome.maxWaterHeight)
                        {
                            AllowedBiomes.Add(biome);
                        }
                    }
                }
            }
        }

        if (AllowedBiomes.Count <= 0)
        {
            AllowedBiomes.Add(errorBiome);
            Debug.LogError("Tile at position: x:" + x + ", y:" + y + " Had to be given a error biome. Height: " + height + " Wetness: " + wetness + " Tempature: " + tempature + " WaterLevel: " + waterLevel);
        }
        mapData.landscapeTiles[x, y] = new LandscapeTile(AllowedBiomes[Random.Range(0,AllowedBiomes.Count)], height, new Vector2Int(x,y), tempature, wetness, waterLevel);
    }

    public IEnumerator GenerateBorderWall(Vector2Int mapSize)
    {
        System.DateTime loopStartTime = System.DateTime.Now;
        float solidWall = wallWidth / 3f;
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0, i = 0; y < wallWidth; y++, i++)
            {
                int random = Random.Range((int)solidWall, i + 1);
                if (random == i || i < solidWall) mapData.landscapeTiles[x, y].biome = wallBiome;
                if ((System.DateTime.Now - loopStartTime).TotalSeconds >= 1)
                {
                    GameGenerator.updateLoadText.Invoke("Progress generating wall at tile: X=" + x + " Y=" + y);
                    yield return null;
                    loopStartTime = System.DateTime.Now;
                }
            }
        }

        for (int y = 0; y < mapSize.y; y++)
        {
            for (int x = 0, i = 0; x < wallWidth; x++, i++)
            {
                int random = Random.Range((int)solidWall, i + 1);
                if (random == i || i < solidWall) mapData.landscapeTiles[x, y].biome = wallBiome;
                if ((System.DateTime.Now - loopStartTime).TotalSeconds >= 1)
                {
                    GameGenerator.updateLoadText.Invoke("Progress generating wall at tile: X=" + x + " Y=" + y);
                    yield return null;
                    loopStartTime = System.DateTime.Now;
                }
            }
        }

        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = mapSize.y - 1, i = 0; y > mapSize.y - wallWidth - 1; y--, i++)
            {
                int random = Random.Range((int)solidWall, i + 1);
                if (random == i || i < solidWall) mapData.landscapeTiles[x, y].biome = wallBiome;
                if ((System.DateTime.Now - loopStartTime).TotalSeconds >= 1)
                {
                    GameGenerator.updateLoadText.Invoke("Progress generating wall at tile: X=" + x + " Y=" + y);
                    yield return null;
                    loopStartTime = System.DateTime.Now;
                }
            }
        }

        for (int y = 0; y < mapSize.y; y++)
        {
            for (int x = mapSize.x - 1, i = 0; x > mapSize.x - wallWidth - 1; x--, i++)
            {
                int random = Random.Range((int)solidWall, i + 1);
                if (random == i || i < solidWall) mapData.landscapeTiles[x, y].biome = wallBiome;
                if ((System.DateTime.Now - loopStartTime).TotalSeconds >= 1)
                {
                    GameGenerator.updateLoadText.Invoke("Progress generating wall at tile: X=" + x + " Y=" + y);
                    yield return null;
                    loopStartTime = System.DateTime.Now;
                }
            }
        }
    }

    public IEnumerator PlaceLandscape(Vector2Int mapSize)
    {
        if (!landscapeTilemap)
        {
            GameGenerator.updateLoadText.Invoke("Tilemap not set");
            Debug.LogError("Tilemap not set");
            yield break;
        }

        landscapeTilemap.ClearAllTiles();
        GameGenerator.updateLoadText.Invoke("Tilemap cleared");
        yield return null;

        System.DateTime loopStartTime = System.DateTime.Now;
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                PlaceTile(x, y);
                if ((System.DateTime.Now - loopStartTime).TotalSeconds >= 1)
                {
                    GameGenerator.updateLoadText.Invoke("Progress placing landscape at tile: X=" + x + " Y=" + y);
                    yield return null;
                    loopStartTime = System.DateTime.Now;
                }
            }
        }
    }

    public void PlaceTile(int x, int y)
    {
        LandscapeTile landscapeTile = mapData.landscapeTiles[x, y];
        if (landscapeTile.biome)
        {
            if (landscapeTile.biome.tile)
            {
                landscapeTilemap.SetTile(new Vector3Int(x, y, 0), landscapeTile.biome.tile);
            }
            else
            {
                Debug.LogError("Tile at position: x:" + x + ", y:" + y + " has a biome, with no tile.");
            }
        }
        else
        {
            Debug.LogError("Tile at position: x:" + x + ", y:" + y + " has no biome");
        }
    }
}
