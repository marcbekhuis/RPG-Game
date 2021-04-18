using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(MapData))]
public class ResourceGenerator : MonoBehaviour
{
    [SerializeField] private GenerationResource[] resources;
    [SerializeField] private Tilemap resourceLargeTilemap;
    [SerializeField] private Tilemap resourceLargeShadowTilemap;
    [SerializeField] private Tilemap resourceSmallShadowTilemap;
    [SerializeField] private Tilemap resourceSmallTilemap;

    private MapData mapData;
    private Dictionary<Biome, List<GenerationResource>> resourcesFilteredByBiome = new Dictionary<Biome, List<GenerationResource>>();

    public IEnumerator MakeResources(Vector2Int mapSize)
    {
        mapData = GetComponent<MapData>();

        if (resources.Length <= 0)
        {
            GameGenerator.updateLoadText.Invoke("No resources in resource list");
            Debug.LogError("No resources in resource list");
            yield break;
        }
        yield return StartCoroutine(GenerateResources(mapSize));
        yield return StartCoroutine(PlaceResources(mapSize));
    }

    public IEnumerator GenerateResources(Vector2Int mapSize)
    {
        mapData.resources = new Resource[mapSize.x, mapSize.y];

        GameGenerator.updateLoadText.Invoke("Starting landscape Generation");
        System.DateTime loopStartTime = System.DateTime.Now;

        foreach (var resource in resources)
        {
            foreach (var biome in resource.biomes)
            {
                if (resourcesFilteredByBiome.ContainsKey(biome)) resourcesFilteredByBiome[biome].Add(resource);
                else resourcesFilteredByBiome.Add(biome, new List<GenerationResource> { resource });
            }
        }

        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                GenerateResource(x, y);
                if ((System.DateTime.Now - loopStartTime).TotalSeconds >= 1)
                {
                    GameGenerator.updateLoadText.Invoke("Progress resource generation at tile: X=" + x + " Y=" + y);
                    yield return null;
                    loopStartTime = System.DateTime.Now;
                }
            }
        }
    }

    private void GenerateResource(int x, int y)
    {
        Biome biome = mapData.landscapeTiles[x, y].biome;
        if (!resourcesFilteredByBiome.ContainsKey(biome)) return;
        int numberOfResources = resourcesFilteredByBiome[biome].Count;
        if (numberOfResources <= 0) return;
        int resourceIndex = Random.Range(0, numberOfResources);
        for (int i = 0; i < numberOfResources; i++)
        {
            int change = Random.Range(0, 100);
            if(change < resourcesFilteredByBiome[biome][resourceIndex].spawnChange)
            {
                mapData.resources[x, y] = new Resource(resourcesFilteredByBiome[biome][resourceIndex].resource);
                return;
            }
            if (resourceIndex < numberOfResources - 1) resourceIndex++;
            else resourceIndex = 0;
        }
    }

    public IEnumerator PlaceResources(Vector2Int mapSize)
    {
        if (!resourceLargeTilemap)
        {
            GameGenerator.updateLoadText.Invoke("Tilemap not set");
            Debug.LogError("Tilemap not set");
            yield break;
        }

        resourceLargeTilemap.ClearAllTiles();
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
                    GameGenerator.updateLoadText.Invoke("Progress placing resource at tile: X=" + x + " Y=" + y);
                    yield return null;
                    loopStartTime = System.DateTime.Now;
                }
            }
        }
    }

    public void PlaceTile(int x, int y)
    {
        Resource resource = mapData.resources[x, y];
        if (resource == null) return;
        Tilemap resourceTilemap;
        Tilemap resourceShadowTilemap;
        if (resource.information.size == ESize.Large)
        {
            resourceTilemap = resourceLargeTilemap;
            resourceShadowTilemap = resourceLargeShadowTilemap;
        }
        else
        {
            resourceTilemap = resourceSmallTilemap;
            resourceShadowTilemap = resourceSmallShadowTilemap;
        }

        Vector3 positionOffset = new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(-0.4f, 0.4f), 0);
        Vector3 SpriteSize = Vector3.one * Random.Range(0.5f, 1);
        resourceTilemap.SetTile(new Vector3Int(x, y, 0), resource.information.tile);
        resourceTilemap.SetTransformMatrix(new Vector3Int(x, y, 0), Matrix4x4.TRS(positionOffset, Quaternion.Euler(0, 0, 0), SpriteSize));
        resourceShadowTilemap.SetTile(new Vector3Int(x, y, 0), resource.information.tile);
        resourceShadowTilemap.SetTransformMatrix(new Vector3Int(x, y, 0), Matrix4x4.TRS(positionOffset, Quaternion.Euler(0, 0, 0), SpriteSize));
    }
}
