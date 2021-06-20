using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "Structures/Resource")]
public class Resource : Structure
{
    public RandomItem[] drops;

    public Resource(Resource resource)
    {
        this.tile = resource.tile;
        this.drops = resource.drops;
    }

    public Resource()
    {
    }
}
