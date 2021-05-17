using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New item", menuName = "ScriptableObjects/Item")]
public class ItemInformation : ScriptableObject
{
    public Sprite sprite;
    [Min(1)] public int maxStackSize = 10;
    public string description = "A basic item.";
}
