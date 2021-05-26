using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected GameObject itemContainerPrefab;
    public Inventory inventory;
    [SerializeField] protected RectTransform itemContainerHolder;

    [HideInInspector] public List<ItemContainer> itemContainers = new List<ItemContainer>();

    public Inventory Inventory
    {
        get => inventory;
        set
        {
            if(inventory) inventory.OnInventoryChanged -= UpdateDisplay;
            inventory = value;
            if (inventory) inventory.OnInventoryChanged += UpdateDisplay;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        if(inventory) inventory.OnInventoryChanged += UpdateDisplay;
    }

    public virtual void UpdateDisplay()
    {
        CreateContainers();
        RemoveContainers();
        UpdateContainers();
    }

    public void UpdateContainers()
    {
        for (int i = 0; i < itemContainers.Count; i++)
        {
            if (i < inventory.items.Count)
            {
                itemContainers[i].inventory = inventory;
                itemContainers[i].AddItem(inventory.items[i], i);
            }
            else itemContainers[i].RemoveItem();
        }
    }

    public void CreateContainers()
    {
        for (int i = itemContainers.Count; i < inventory.Slots; i++)
        {
            ItemContainer itemContainer = Instantiate(itemContainerPrefab, itemContainerHolder).GetComponent<ItemContainer>();
            itemContainer.Setup(null, i, inventory, this);
            itemContainers.Add(itemContainer);
        }
    }

    public void RemoveContainers()
    {
        while(itemContainers.Count > inventory.Slots)
        {
            Destroy(itemContainers[itemContainers.Count - 1].gameObject);
            itemContainers.RemoveAt(itemContainers.Count - 1);
        }
    }

    public bool OnContainer(Vector2 position)
    {
        Vector2 halfSize = itemContainerHolder.rect.size * 0.5f * CanvasScaleHelper.instance.GetCanvasScale();
        Vector2 centerPosition = itemContainerHolder.position;
        float highPosition = centerPosition.y + halfSize.y;
        float lowPosition = centerPosition.y - halfSize.y;
        float leftPosition = centerPosition.x - halfSize.x;
        float rightPosition = centerPosition.x + halfSize.x;
        if (position.x < rightPosition && position.x > leftPosition && position.y < highPosition && position.y > lowPosition) return true;
        else return false;
    }

    public bool OnItemContainer(Vector2 position, out ItemContainer itemContainer)
    {
        foreach (var itemContainerLoop in itemContainers)
        {
            if (!itemContainerLoop.OnContainer(position)) continue;
            itemContainer = itemContainerLoop;
            return true;
        }
        itemContainer = default;
        return false;
    }

    public bool OnItemContainerWithItem(Vector2 position, out ItemContainer itemContainer)
    {
        foreach (var itemContainerLoop in itemContainers)
        {
            if (!itemContainerLoop.HasItem()) continue;
            if (!itemContainerLoop.OnContainer(position)) continue;
            itemContainer = itemContainerLoop;
            return true;
        }
        itemContainer = default;
        return false;
    }
}
