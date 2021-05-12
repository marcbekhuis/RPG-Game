using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandscapeTile
{
    public Biome biome;
    public float tempature;
    public float wetness;
    public float landHeight;
    public float waterHeight;
    public Vector2Int position;

    public LandscapeTile(Biome biome, float landHeight, Vector2Int position, float tempature = 20, float wetness = 50, float waterHeight = 0)
    {
        this.biome = biome;
        this.tempature = tempature;
        this.wetness = wetness;
        this.landHeight = landHeight;
        this.waterHeight = waterHeight;
        this.position = position;
    }
}
