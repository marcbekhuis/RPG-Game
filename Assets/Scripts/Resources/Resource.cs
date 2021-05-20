using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Resource : Structure
{
    new public ResourceInformation information;
    public Resource(ResourceInformation information) : base(information)
    {
        this.information = information;
    }
}
