using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    void Setup(GameObject owner, Vector2 direction, float angle);
}
