using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeDetection : MonoBehaviour
{
    [SerializeField] private float radius = 1;
    [SerializeField] private LayerMask layerMasks;
    [SerializeField] private Transform DetectionBase;

    public bool DetectRange(Vector2 direction)
    {
        RaycastHit2D[] raycastHit2Ds = Physics2D.CircleCastAll(DetectionBase.position, radius, direction, radius, layerMasks);
        foreach (var raycastHit2D in raycastHit2Ds)
        {
            if (raycastHit2D.transform == transform) continue;
            else if (raycastHit2D.transform.parent == transform) continue;
            else return true;
        }
        return false;
    }

    public Vector2 DetectRangePosition(Vector2 direction)
    {
        RaycastHit2D[] raycastHit2Ds = Physics2D.CircleCastAll(DetectionBase.position, radius, direction, radius, layerMasks);
        foreach (var raycastHit2D in raycastHit2Ds)
        {
            if (raycastHit2D.transform == transform) continue;
            else if (raycastHit2D.transform.parent == transform) continue;
            else return raycastHit2D.point;
        }
        return default;
    }
}
