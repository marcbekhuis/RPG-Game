using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New structure", menuName = "Structures/Structure")]
public class Structure : ScriptableObject
{
    public TileBase tile;

    public Structure(Structure structure)
    {
        this.tile = structure.tile;
    }

    public Structure()
    {

    }

    public virtual Structure Create(Structure structure)
    {
        return new Structure(structure);
    }
}
