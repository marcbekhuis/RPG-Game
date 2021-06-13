using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemMovement : MonoBehaviour
{
    [SerializeField] private InventoryDisplay[] inventoryDisplays;

    private ItemContainer selectedItemContainer;

    private void Update()
    {
        if (InputManager.instance.GetKeyUp(InputActions.MoveItem))
        {
            if (selectedItemContainer)
            {
                if (IsMouseOnContainer(Input.mousePosition, out ItemContainer itemContainer))
                {
                    if (itemContainer.inventory.HasSpace())
                    {
                        if (itemContainer.HasItem())
                        {
                            Item item = selectedItemContainer.item;
                            if (selectedItemContainer != itemContainer)
                            {
                                if (itemContainer.inventory.SlotHasItem(item, itemContainer.inventoryIndex))
                                {
                                    itemContainer.inventory.AddItemToSlot(item, itemContainer.inventoryIndex);
                                    selectedItemContainer.inventory.RemoveItem(item);
                                }
                                else if (selectedItemContainer.inventory == itemContainer.inventory)
                                {
                                    itemContainer.inventory.MoveItemToIndex(item, itemContainer.inventoryIndex);
                                }
                                else
                                {
                                    selectedItemContainer.inventory.RemoveItem(item);
                                    itemContainer.inventory.AddItemAtIndex(item, itemContainer.inventoryIndex);
                                }
                            }
                            else selectedItemContainer.inventoryDisplay.UpdateDisplay();
                        }
                        else
                        {
                            Item item = selectedItemContainer.item;
                            if (selectedItemContainer.inventory == itemContainer.inventory)
                            {
                                itemContainer.inventory.MoveItemToLast(item);
                            }
                            else
                            {
                                selectedItemContainer.inventory.RemoveItem(item);
                                itemContainer.inventory.AddItemExact(item);
                            }
                        }
                    }
                    else selectedItemContainer.inventoryDisplay.UpdateDisplay();
                    if (selectedItemContainer.itemDisplay) selectedItemContainer.itemDisplay.RemoveCanvas();
                    selectedItemContainer = null;
                    return;
                }
                else
                {
                    foreach (var inventoryDisplay in inventoryDisplays)
                    {
                        if (inventoryDisplay.OnContainer(Input.mousePosition))
                        {
                            if (inventoryDisplay.inventory.HasSpace())
                            {
                                Item item = selectedItemContainer.item;
                                if (selectedItemContainer.inventory == inventoryDisplay.inventory)
                                {
                                    inventoryDisplay.inventory.MoveItemToLast(item);
                                }
                                else
                                {
                                    selectedItemContainer.inventory.RemoveItem(item);
                                    inventoryDisplay.inventory.AddItemExact(item);
                                }
                            }
                            else selectedItemContainer.inventoryDisplay.UpdateDisplay();
                            if (selectedItemContainer.itemDisplay) selectedItemContainer.itemDisplay.RemoveCanvas();
                            selectedItemContainer = null;
                            return;
                        }
                    }
                }
            }
        }
        else if (InputManager.instance.GetKeyDown(InputActions.MoveItem))
        {
            if (selectedItemContainer) return;
            if (IsMouseOnContainerWithItem(Input.mousePosition, out ItemContainer itemContainer))
            {
                selectedItemContainer = itemContainer;
                selectedItemContainer.itemDisplay.AddCanvas();
            }
        }
        else if(selectedItemContainer)
        {
            selectedItemContainer.itemDisplay.SetPosition(Input.mousePosition);
        }
    }

    private bool IsMouseOnContainerWithItem(Vector2 position, out ItemContainer itemContainer)
    {
        foreach (var inventoryDisplay in inventoryDisplays)
        {
            if (inventoryDisplay.OnItemContainerWithItem(position, out itemContainer)) return true;
        }
        itemContainer = default;
        return false;
    }

    private bool IsMouseOnContainer(Vector2 position, out ItemContainer itemContainer)
    {
        foreach (var inventoryDisplay in inventoryDisplays)
        {
            if (inventoryDisplay.OnItemContainer(position, out itemContainer)) return true;
        }
        itemContainer = default;
        return false;
    }
}
