using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipHandler : MonoBehaviour
{
    [SerializeField] private InventoryDisplay[] inventoryDisplays;

    void Update()
    {
        if (!Input.anyKey && IsMouseOnContainerWithItem(Input.mousePosition, out Item item))
        {
            Tooltip.instance.SetDisplayItem(item);
            Tooltip.instance.ShowTooltip();
            Tooltip.instance.SetPosition(Input.mousePosition);
        }
        else
        {
            Tooltip.instance.HideTooltip();
            return;
        }
    }

    private void OnDisable()
    {
        Tooltip.instance.HideTooltip();
    }

    private bool IsMouseOnContainerWithItem(Vector2 position, out Item item)
    {
        foreach (var inventoryDisplay in inventoryDisplays)
        {
            if (inventoryDisplay.OnItemContainerWithItem(position, out ItemContainer itemContainer))
            {
                item = itemContainer.item;
                return true;
            }
        }
        item = default;
        return false;
    }
}
