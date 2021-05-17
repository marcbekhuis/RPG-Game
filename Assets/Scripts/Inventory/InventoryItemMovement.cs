using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItemMovement : MonoBehaviour
{
    [SerializeField] private InventoryDisplay[] inventoryDisplays;

    private GameObject hoveredItem;
    private int itemIndex;
    private InventoryDisplay itemInventoryDisplay;
    private Canvas itemCanvas;

    // Update is called once per frame
    void Update()
    {
        if (InputManager.instance.GetKeyUp(InputActions.MoveItem))
        {
            if (!hoveredItem) return;
            Destroy(itemCanvas);
            GameObject uiObject = GetPointerOverUIObject();
            Debug.LogError("uiObject" + uiObject.name);
            if (!uiObject) return;
            InventoryDisplay newInventoryDisplays = itemInventoryDisplay;
            foreach (var inventoryDisplay in inventoryDisplays)
            {
                if (inventoryDisplay.gameObject == uiObject)
                {
                    newInventoryDisplays = inventoryDisplay;
                    break;
                }
            }
            if (newInventoryDisplays == itemInventoryDisplay) itemInventoryDisplay.inventory.MoveItemInInventory(itemIndex, GetIndexInInventory(itemInventoryDisplay));
            else
            {
                newInventoryDisplays.inventory.AddItem(itemInventoryDisplay.inventory.items[itemIndex], GetIndexInInventory(newInventoryDisplays));
                itemInventoryDisplay.inventory.RemoveItemAtIndex(itemIndex);
            }
        }
        else if (InputManager.instance.GetKey(InputActions.MoveItem))
        {
            if (!hoveredItem) return;
            if (!itemCanvas) {
                itemCanvas = hoveredItem.AddComponent<Canvas>();
                itemCanvas.overrideSorting = true;
                itemCanvas.sortingOrder = 10;
            }
            hoveredItem.transform.position = Input.mousePosition;
            return;
        }
        else
        {
            GameObject newHoverItem = GetItemGameObject();
            if (hoveredItem == newHoverItem) return;
            hoveredItem = newHoverItem;
            if (!hoveredItem) return;
            GetItemFromGameObject();
        }
    }

    public int GetIndexInInventory(InventoryDisplay inventoryDisplay)
    {
        int index = 0;
        foreach (RectTransform child in inventoryDisplay.transform)
        {
            Vector2 sizeHalf = child.sizeDelta / 2f;
            Vector2 slotPosition = child.position;
            Vector2 mousePosition = Input.mousePosition;
            if (!slotPosition.Equals(mousePosition))
            {
                Debug.LogError(inventoryDisplay.name + " Mouse: " + mousePosition + " Slot: " + slotPosition + " Index: " + index);
                if (mousePosition.x > slotPosition.x - sizeHalf.x && mousePosition.x < slotPosition.x + sizeHalf.x && mousePosition.y > slotPosition.y - sizeHalf.y && mousePosition.y < slotPosition.y + sizeHalf.y)
                {
                    return index;
                }
            }
            index++;
        }
        return index;
    }

    public GameObject GetPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        foreach (var result in results)
        {
            if(result.gameObject.activeSelf) return result.gameObject;
        }
        return null;
    }

    public GameObject GetItemGameObject()
    {
        GameObject uiObject = GetPointerOverUIObject();
        if (!uiObject) return null;
        if (uiObject.name.Contains("Icon") || uiObject.name.Contains("Amount")) return uiObject.transform.parent.gameObject;
        return null;
    }

    public void GetItemFromGameObject()
    {
        itemIndex = hoveredItem.transform.GetSiblingIndex();
        Transform itemParent = hoveredItem.transform.parent;
        foreach (var inventoryDisplay in inventoryDisplays)
        {
            if(inventoryDisplay.gameObject.transform == itemParent)
            {
                itemInventoryDisplay = inventoryDisplay;
                break;
            }
        }
    }
}
