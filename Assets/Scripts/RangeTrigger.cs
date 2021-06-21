using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RangeTrigger : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private LayerMask layerMasks;
    [SerializeField] private UnityEvent OnDetection;

    // Update is called once per frame
    void Update()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, radius, layerMasks);
        foreach (var collider2D in collider2Ds)
        {
            if (collider2D.transform == transform) continue;
            else if (collider2D.transform.parent == transform) continue;
            OnDetection.Invoke();
        }
    }
}
