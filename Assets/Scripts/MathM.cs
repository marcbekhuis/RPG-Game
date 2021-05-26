using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathM
{
    public static bool CheckIfInArc(float centerAngle, float targetAngle, float arc)
    {
        float halfArc = arc / 2f;
        if (targetAngle > centerAngle - halfArc && targetAngle < centerAngle + halfArc) return true;
        else if (targetAngle + 360 > centerAngle - halfArc && targetAngle + 360 < centerAngle + halfArc) return true;
        else if (targetAngle > centerAngle + 360  - halfArc && targetAngle < centerAngle + 360 + halfArc) return true;
        else return false;
    }

    public static float GetAngle(Vector2 direction)
    {
        float angle180 = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float angle360 = 0;
        if (angle180 < 0)
        {
            angle360 = 360 + angle180;
        }
        else angle360 = angle180;
        return angle360;
    }
}
