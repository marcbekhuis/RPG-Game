using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{
    public static MapData instance;
    public LandscapeTile[,] landscapeTiles;
    public Structure[,] structures;
    public Resource[,] resources;
    [HideInInspector]public Vector2Int size;

    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }
}
