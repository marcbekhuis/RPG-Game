using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemContainer : MonoBehaviour
{
    [HideInInspector] public Inventory inventory;
    [HideInInspector] public InventoryDisplay inventoryDisplay;
    [HideInInspector] public Item item;
    [HideInInspector] public ItemDisplay itemDisplay;
    [HideInInspector] public int inventoryIndex;
    [HideInInspector] public Image backgroundImage;

    [SerializeField] private GameObject itemDisplayPrefab;

    private RectTransform rectTransform;

    public void Setup(Item item, int index, Inventory inventory, InventoryDisplay inventoryDisplay)
    {
        this.inventory = inventory;
        this.inventoryDisplay = inventoryDisplay;
        rectTransform = GetComponent<RectTransform>();
        backgroundImage = GetComponent<Image>();
        AddItem(item, index);
    }

    public void AddItem(Item item, int index)
    {
        this.item = item;
        inventoryIndex = index;
        if (itemDisplay)
        {
            itemDisplay.SetItem(item);
        }
        else if(this.item)
        {
            GameObject itemDisplayGameObject = Instantiate(itemDisplayPrefab, transform);
            itemDisplay = itemDisplayGameObject.GetComponent<ItemDisplay>();
            itemDisplay.Setup(item, this, inventory, inventoryDisplay);
        }
    }

    public void RemoveItem()
    {
        if (!item) return;
        Destroy(itemDisplay.gameObject);
        itemDisplay = null;
        item = null;
    }

    public bool HasItem()
    {
        return item;
    }

    public bool OnContainer(Vector2 position)
    {
        Vector2 halfSize = rectTransform.sizeDelta * 0.5f * CanvasScaleHelper.instance.GetCanvasScale();
        float highPosition = rectTransform.position.y + halfSize.y;
        float lowPosition = rectTransform.position.y - halfSize.y;
        float leftPosition = rectTransform.position.x - halfSize.x;
        float rightPosition = rectTransform.position.x + halfSize.x;
        if (position.x < rightPosition && position.x > leftPosition && position.y < highPosition && position.y > lowPosition) return true;
        else return false;
    }

    public void SetBackgroundColor(Color color)
    {
        backgroundImage.color = color;
    }
}
