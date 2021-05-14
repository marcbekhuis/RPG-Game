using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{
    public LandscapeTile[,] landscapeTiles;
    public Structure[,] structures;
    public Resource[,] resources;
    [HideInInspector]public Vector2Int size;
}
