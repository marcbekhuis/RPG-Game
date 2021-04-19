using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField][Range(0,360)] private float arc = 90;
    [SerializeField] private float radius = 1;
    [SerializeField] private LayerMask layerMasks;
    [SerializeField] private Transform attackBase;

    public void AttackCall(Vector2 direction, float attackdamage)
    {
       RaycastHit2D[] raycastHit2Ds =  Physics2D.CircleCastAll(attackBase.position, radius, direction, radius, layerMasks);
        foreach (var raycastHit2D in raycastHit2Ds)
        {
            if (raycastHit2D.transform == transform) continue;
            else if (raycastHit2D.transform.parent == transform) continue;
            else if (!CheckIfInArg(direction, raycastHit2D.point)) continue;
            else if (raycastHit2D.transform.TryGetComponent<HealthSystem>(out HealthSystem healthSystem))
            {
                Debug.LogError(healthSystem.name);
                healthSystem.RemoveHealth(attackdamage);
            }
            else continue;
        }
    }

    private bool CheckIfInArg(Vector2 direction, Vector2 enityPosition)
    {
        float centerAngle = Vector2.Angle(attackBase.transform.position, (Vector2)attackBase.transform.position + direction);
        float enityAngle = Vector2.Angle(attackBase.transform.position, enityPosition);
        float halfArc = arc / 2;
        if (enityAngle > centerAngle - halfArc && enityAngle < centerAngle - halfArc) return true;
        else return false;
    }
}
