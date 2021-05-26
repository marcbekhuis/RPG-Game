using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ResourceRemoveSystem : Singleton<ResourceRemoveSystem>
{
    [SerializeField] private Tilemap resourceLargeTilemap;
    [SerializeField] private Tilemap resourceLargeShadowTilemap;
    [SerializeField] private Tilemap resourceSmallShadowTilemap;
    [SerializeField] private Tilemap resourceSmallTilemap;

    public void RemoveResource(Vector2Int gridPosition)
    {
        switch (MapData.instance.resources[gridPosition.x, gridPosition.y].size)
        {
            case ESize.Small:
                resourceSmallTilemap.SetTile((Vector3Int)gridPosition, null);
                resourceSmallShadowTilemap.SetTile((Vector3Int)gridPosition, null);
                break;
            case ESize.Medium:
                break;
            case ESize.Large:
                resourceLargeTilemap.SetTile((Vector3Int)gridPosition, null);
                resourceLargeShadowTilemap.SetTile((Vector3Int)gridPosition, null);
                break;
            default:
                break;
        }
        MapData.instance.resources[gridPosition.x, gridPosition.y] = null;
    }
}
