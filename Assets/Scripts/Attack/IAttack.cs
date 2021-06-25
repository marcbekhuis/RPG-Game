using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    void AttackCall(Vector2 mousePosition, float cooldown);
}
