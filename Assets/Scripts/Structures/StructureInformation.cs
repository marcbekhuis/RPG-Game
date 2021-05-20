using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New structure", menuName = "ScriptableObjects/Structure")]
public class StructureInformation : ScriptableObject
{
    public TileBase tile;
}
