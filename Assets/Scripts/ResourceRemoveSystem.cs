using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ResourceRemoveSystem : Singleton<ResourceRemoveSystem>
{
    [SerializeField] private Tilemap resourceTilemap;
    [SerializeField] private Tilemap resourceShadowTilemap;

    public void RemoveResource(Vector2Int gridPosition)
    {
        resourceTilemap.SetTile((Vector3Int)gridPosition, null);
        resourceShadowTilemap.SetTile((Vector3Int)gridPosition, null);
        MapData.instance.resources[gridPosition.x, gridPosition.y] = null;
    }
}
