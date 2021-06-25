using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestingAttack : MonoBehaviour, IAttack
{
    [SerializeField] [Range(0, 360)] private float arc = 90;
    [SerializeField] [Min(0)] private float radius = 1;
    [SerializeField] private LayerMask layerMasks;
    [SerializeField] private Transform attackBase;
    [SerializeField] private Inventory inventory;
    [SerializeField] private Transform handRotation;
    [Header("Debugging")]
    [SerializeField] private Color circleColor = Color.red;

    public void AttackCall(Vector2 mousePosition, float cooldown)
    {
        StopAllCoroutines();
        if (handRotation) StartCoroutine(Animation(cooldown));
        RaycastHit2D[] raycastHit2ds = Physics2D.CircleCastAll(attackBase.position, radius, Vector2.up, 0, layerMasks);
        foreach (var raycastHit2d in raycastHit2ds)
        {
            if (raycastHit2d.transform == transform) continue;
            else if (raycastHit2d.transform.parent == transform) continue;
            else if (!MathM.CheckIfInArc(MathM.GetAngle(mousePosition - (Vector2)attackBase.position), MathM.GetAngle(raycastHit2d.point - (Vector2)attackBase.position), arc)) continue;
            else if (HarvestResource(raycastHit2d.point)) return;
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

    public bool HarvestResource(Vector2 position)
    {
        Vector2Int gridPosition = Vector2Int.RoundToInt(position - Vector2.one * 0.5f);
        if(MapData.instance.resources[gridPosition.x, gridPosition.y])
        {
            foreach (var drop in MapData.instance.resources[gridPosition.x, gridPosition.y].drops)
            {
                inventory.AddItem(drop.item, Random.Range(drop.minAmount, drop.maxAmount + 1));
            }
            ResourceRemoveSystem.instance.RemoveResource(gridPosition);
            return true;
        }
        return false;
    }

    public void SetSettings(float arc, float radius)
    {
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
