using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Placetreesrandom : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase[] tiles;

    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < 50; x++)
        {
            for (int y = 0; y < 50; y++)
            {
                TileBase tileToPlace = tiles[Random.Range(0, tiles.Length)];
                tilemap.SetTile(new Vector3Int(x,y,0), tileToPlace);
                tilemap.SetTransformMatrix(new Vector3Int(x, y, 0), Matrix4x4.TRS(new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(-0.4f, 0.4f), 0), Quaternion.Euler(0,0,0),Vector3.one * Random.Range(0.5f, 1)));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
