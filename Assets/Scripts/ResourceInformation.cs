using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "ScriptableObjects/Resource")]
public class ResourceInformation : StructureInformation
{
    public ESize size;
    public RandomItem[] drops;
}
