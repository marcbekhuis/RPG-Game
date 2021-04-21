using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] [Min(0)] private float attackDamage = 1;
    [SerializeField] [Range(0,360)] private float arc = 90;
    [SerializeField] [Min(0)] private float radius = 1;
    [SerializeField] private LayerMask layerMasks;
    [SerializeField] private Transform attackBase;
    [Header("Debugging")]
    [SerializeField] private Color circleColor = Color.red;

    public void AttackCall(Vector2 direction)
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(attackBase.position, radius, layerMasks);
        foreach (var collider2D in collider2Ds)
        {
            if (collider2D.transform == transform) continue;
            else if (collider2D.transform.parent == transform) continue;
            else if (!CheckIfInArg(direction, collider2D.transform.position)) continue;
            else if (collider2D.transform.TryGetComponent<HealthSystem>(out HealthSystem healthSystem)) healthSystem.RemoveHealth(attackDamage);
            else continue;
        }
    }

    private bool CheckIfInArg(Vector2 direction, Vector2 enityPosition)
    {
        float centerAngle = Vector2.Angle(attackBase.transform.position, direction);
        Debug.DrawLine(attackBase.transform.position, direction, circleColor, 1);
        float enityAngle = Vector2.Angle(attackBase.transform.position, enityPosition);
        Debug.DrawLine(attackBase.transform.position, enityPosition, circleColor, 1);
        float halfArc = arc / 2f;
        if (enityAngle > centerAngle - halfArc && enityAngle < centerAngle + halfArc) return true;
        else return false;
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = circleColor;
        Gizmos.DrawWireSphere(attackBase.transform.position, radius);
    }
}
