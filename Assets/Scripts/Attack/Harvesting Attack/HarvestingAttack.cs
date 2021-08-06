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
        Vector2Int[] positions = GetTilesInRange(attackBase.position, radius);
        if (positions.Length <= 0) return;
        Vector2Int closestPosition = new Vector2Int(-500,-500);
        float closestDistance = 1000;
        foreach (var position in positions)
        {
            if (MathM.CheckIfInArc(MathM.GetAngle(mousePosition - (Vector2)attackBase.position), MathM.GetAngle(position - (Vector2)attackBase.position), arc)) 
            {
                if (Vector2.Distance(attackBase.position, position) < closestDistance) closestPosition = position;
            }
        }
        if (closestPosition == new Vector2Int(-500, -500)) return;
        HarvestResource(closestPosition);
    }

    private Vector2Int[] GetTilesInRange(Vector2 center, float radius)
    {
        List<Vector2Int> positions = new List<Vector2Int>();
        for (int x = Mathf.FloorToInt(center.x - radius); x <= Mathf.CeilToInt(center.x + radius); x++)
        {
            for (int y = Mathf.FloorToInt(center.y - radius); y <= Mathf.CeilToInt(center.y + radius); y++)
            {
                if (!MapData.instance.resources[x, y]) continue;
                Vector2Int position = new Vector2Int(x, y);
                if (Vector2.Distance(center, position + Vector2.one * 0.5f) <= radius) positions.Add(position);
            }
        }
        return positions.ToArray();
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

    public bool HarvestResource(Vector2Int gridPosition)
    {
        if(MapData.instance.resources[gridPosition.x, gridPosition.y])
        {
            foreach (var drop in MapData.instance.resources[gridPosition.x, gridPosition.y].drops)
            {
                int amount = Random.Range(drop.minAmount, drop.maxAmount + 1);
                if(amount > 0) inventory.AddItem(drop.item, amount);
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
