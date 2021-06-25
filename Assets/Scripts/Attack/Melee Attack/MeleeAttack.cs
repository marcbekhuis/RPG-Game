using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour, IAttack
{
    [SerializeField] [Min(0)] private float attackDamage = 1;
    [SerializeField] [Range(0,360)] private float arc = 90;
    [SerializeField] [Min(0)] private float radius = 1;
    [SerializeField] private LayerMask layerMasks;
    [SerializeField] private Transform attackBase;
    [SerializeField] private Transform handRotation;
    [Header("Debugging")]
    [SerializeField] private Color circleColor = Color.red;

    public void AttackCall(Vector2 mousePosition, float cooldown)
    {
        StopAllCoroutines();
        if (handRotation) StartCoroutine(Animation(cooldown));
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(attackBase.position, radius, layerMasks);
        foreach (var collider2D in collider2Ds)
        {
            if (collider2D.transform == transform) continue;
            else if (collider2D.transform.parent == transform) continue;
            else if (!MathM.CheckIfInArc(MathM.GetAngle(mousePosition - (Vector2)attackBase.position), MathM.GetAngle((Vector2)collider2D.transform.position - (Vector2)attackBase.position), arc)) continue;
            else if (collider2D.transform.TryGetComponent<HealthSystem>(out HealthSystem healthSystem)) healthSystem.RemoveHealth(attackDamage);
            else continue;
        }
    }

    IEnumerator Animation(float duration)
    {
        float startAngle = arc / 2 * -1;
        float endAngle = arc / 2;

        handRotation.localEulerAngles = new Vector3(0, 0, startAngle);
        for (float time = 0; time < duration; time += Time.deltaTime)
        {
            handRotation.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(startAngle, endAngle, time / duration));
            yield return null;
        }
        handRotation.localEulerAngles = new Vector3(0, 0, endAngle);
        yield return new WaitForSeconds(1);
        handRotation.localEulerAngles = new Vector3(0, 0, 0);
    }

    public void SetSettings(float attackDamage, float arc, float radius)
    {
        this.attackDamage = attackDamage;
        this.arc = arc;
        this.radius = radius;
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = circleColor;
        Gizmos.DrawWireSphere(attackBase.position, radius);
    }
}
