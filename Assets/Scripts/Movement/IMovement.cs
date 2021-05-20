using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement
{
    public void Move(Vector2 direction, bool sprint = false);

    public void Frozen(bool active);
}
