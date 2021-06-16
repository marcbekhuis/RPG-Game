using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingGenerator : MonoBehaviour
{
    [SerializeField] private BuildingEntry[] buildings;
    [SerializeField] private Tilemap structureTilemap;
    [SerializeField] private Tilemap structureShadowTilemap;

    private MapData mapData;

    [System.Serializable]
    public class BuildingEntry 
    {
        public Tilemap building;
        public Biome[] biomes;
        [Range(0,100)] public float change = 50;
    }

    public IEnumerator MakeBuildings(Vector2Int mapSize)
    {
        mapData = GetComponent<MapData>();

        if (buildings.Length <= 0)
        {
            GameGenerator.updateLoadText.Invoke("No building in buildings list");
            Debug.LogError("No building in buildings list");
            yield break;
        }
        yield return StartCoroutine(GenerateBuildings(mapSize));
        yield return StartCoroutine(PlaceBuildings(mapSize));
    }

    public IEnumerator GenerateBuildings(Vector2Int mapSize)
    {
        mapData.structures = new Structure[mapSize.x, mapSize.y];

        GameGenerator.updateLoadText.Invoke("Starting landscape Generation");
        System.DateTime loopStartTime = System.DateTime.Now;

        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                GenerateBuilding(x, y);
                if ((System.DateTime.Now - loopStartTime).TotalSeconds >= 1)
                {
                    GameGenerator.updateLoadText.Invoke("Progress building generation at tile: X=" + x + " Y=" + y);
                    yield return null;
                    loopStartTime = System.DateTime.Now;
                }
            }
        }
    }

    private void GenerateBuilding(int x, int y)
    {
        if(mapData.structures[x,y] == null)
        {
            float randomNumber = Random.Range(0, buildings.Length * 100f);
            float passedValue = 0;
            Tilemap buildTilemap = null;

            foreach (var building in buildings)
            {
                if (randomNumber <= passedValue + building.change)
                {
                    if (buildingValid(x, y, building))
                    {
                        buildTilemap = building.building;
                        break;
                    }
                }
                passedValue += building.change;
            }
            if (!buildTilemap) return;

            for (int x2 = 0; x2 < buildTilemap.cellBounds.size.x; x2++)
            { 
                for (int y2 = 0; y2 < buildTilemap.cellBounds.size.y; y2++)
                {
                    Tile tile = new Tile();
                    tile.sprite = buildTilemap.GetTile<Tile>(new Vector3Int(x2, y2, 0)).sprite;
                    mapData.structures[x + x2, y + y2] = new Structure(tile);
                }
            }
            foreach (Transform child in buildTilemap.gameObject.transform)
            {
                Instantiate(child.gameObject, new Vector3(x,y) + child.localPosition, new Quaternion(0,0,0,0));
            }
        }
    }

    private bool buildingValid(int x, int y, BuildingEntry building)
    {
        if (x + building.building.cellBounds.size.x >= mapData.structures.GetLength(0) || y + building.building.cellBounds.size.y >= mapData.structures.GetLength(1)) return false;
        for (int x2 = 0; x2 < building.building.cellBounds.size.x; x2++)
        {
            for (int y2 = 0; y2 < building.building.cellBounds.size.y; y2++)
            {
                if (mapData.structures[x + x2, y + y2] != null) return false;
                bool invalidBiome = true;
                foreach (var biome in building.biomes)
                {
                    if (mapData.landscapeTiles[x + x2, y + y2].biome == biome) invalidBiome = false;
                }
                if (invalidBiome) return false;
            }
        }
        return true;
    }

    public IEnumerator PlaceBuildings(Vector2Int mapSize)
    {
        if (!structureTilemap)
        {
            GameGenerator.updateLoadText.Invoke("Tilemap not set");
            Debug.LogError("Tilemap not set");
            yield break;
        }

        structureTilemap.ClearAllTiles();
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
        if (!mapData.structures[x, y]) return;
        structureTilemap.SetTile(new Vector3Int(x, y, 0), mapData.structures[x,y].tile);
        structureShadowTilemap.SetTile(new Vector3Int(x, y, 0), mapData.structures[x, y].tile);
    }
}
