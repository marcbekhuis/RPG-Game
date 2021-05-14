using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RangeDetection : MonoBehaviour
{
    [SerializeField] [Min(0)] private float radius = 1;
    [SerializeField] private LayerMask layerMasks;
    [SerializeField] private Transform DetectionBase;
    [Header("Debugging")]
    [SerializeField] private Color circleColor = Color.red;

    public bool DetectRange()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(DetectionBase.position, radius, layerMasks);
        foreach (var collider2D in collider2Ds)
        {
            if (collider2D.transform == transform) continue;
            else if (collider2D.transform.parent == transform) continue;
            else return true;
        }
        return false;
    }

    public float GetRadius()
    {
        return radius;
    }

    public Vector2 DetectRangePosition()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(DetectionBase.position, radius, layerMasks);
        foreach (var collider2D in collider2Ds)
        {
            if (collider2D.transform == transform) continue;
            else if (collider2D.transform.parent == transform) continue;
            else return collider2D.transform.position;
        }
        return transform.position;
    }

    public Collider2D[] DetectRangeColliders()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(DetectionBase.position, radius, layerMasks);
        List<Collider2D> filterCollider2ds = new List<Collider2D>();
        foreach (var collider2D in collider2Ds)
        {
            if (collider2D.transform == transform) continue;
            else if (collider2D.transform.parent == transform) continue;
            else filterCollider2ds.Add(collider2D);
        }
        return filterCollider2ds.ToArray();
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = circleColor;
        Gizmos.DrawWireSphere(DetectionBase.transform.position, radius);
    }
}
