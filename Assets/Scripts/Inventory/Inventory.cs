using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int slots = 10;
    [Tooltip("The items the player starts with")][SerializeField] private List<StartingItem> startingItems = new List<StartingItem>();

    [HideInInspector] public List<Item> items = new List<Item>();

    public delegate void InventoryChanged();
    public event InventoryChanged OnInventoryChanged = delegate { };

    public int Slots {
        get => slots;
        set 
        {
            slots = value;
            OnInventoryChanged();
        }
    }

    [System.Serializable]
    public class StartingItem 
    {
        public Item item;
        [Min(1)]public int amount = 1;
    }

    void Start()
    {
        AddStartingItems();
    }

    private void AddStartingItems()
    {
        foreach (var startingItem in startingItems)
        {
            AddItem(startingItem.item.Create(startingItem.item, this, startingItem.amount));
        }
    }

    public void AddItem(Item item)
    {
        int amountToAdd = item.amount;
        foreach (var loopItem in items)
        {
            if (loopItem.name != item.name) continue;
            if (loopItem.amount < loopItem.maxStackSize)
            {
                int stackAmountAvailable = loopItem.maxStackSize - loopItem.amount;
                int amountCantBeAdded = amountToAdd - stackAmountAvailable;
                int amountCanBeAdded = amountToAdd;
                if (amountCantBeAdded > 0) amountCanBeAdded = amountToAdd - amountCantBeAdded;
                amountToAdd = amountCantBeAdded;
                loopItem.amount += amountCanBeAdded;
            }
        }
        if (amountToAdd > 0)
        {
            for (int i = amountToAdd; i > 0; i -= item.maxStackSize)
            {
                if (items.Count < slots)
                {
                    if (i >= item.maxStackSize) items.Add(item.Create(item, this, item.maxStackSize));
                    else items.Add(item.Create(item, this, i));
                }
            }
        }
        OnInventoryChanged();
    }

    public void AddItemExact(Item item)
    {
        item.inventory = this;
        items.Add(item);
        OnInventoryChanged();
    }

    public void AddItemAtIndex(Item item, int index)
    {
        item.inventory = this;
        items.Insert(index, item);
        OnInventoryChanged();
    }

    public bool HasItemExact(Item item)
    {
        return items.Contains(item);
    }

    public bool HasItem(Item item)
    {
        foreach (var loopItem in items)
        {
            if (loopItem.name == item.name) return true;
        }
        return false;
    }

    public bool HasSpace()
    {
        return items.Count < slots;
    }

    public int GetItemAmount(Item item)
    {
        int amount = 0;
        foreach (var loopItem in items)
        {
            if (loopItem.name == item.name) amount += loopItem.amount;
        }
        return amount;
    }

    public void RemoveItemAt(int index)
    {
        items.RemoveAt(index);
        OnInventoryChanged();
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
        OnInventoryChanged();
    }

    public void RemoveItem(Item item, int amount)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if(items[i].name == item.name)
            {
                int amountLeftToRemove = amount - items[i].amount;
                items[i].amount -= amount;
                if (items[i].amount <= 0)
                {
                    items.RemoveAt(i);
                    i--;
                }
                amount = amountLeftToRemove;
                if (amountLeftToRemove <= 0) break;
            }
        }
        OnInventoryChanged();
    }

    public void RemoveAllItemsByName(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].name == item.name)
            {
                items.RemoveAt(i);
                i--;
            }
        }
        OnInventoryChanged();
    }

    public void RemoveAllItemsByName(string name)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].name == name)
            {
                items.RemoveAt(i);
                i--;
            }
        }
        OnInventoryChanged();
    }

    public void MoveItemToIndex(Item item, int index)
    {
        items.Remove(item);
        items.Insert(index, item);
        OnInventoryChanged();
    }

    public void MoveItemToLast(Item  item)
    {
        items.Remove(item);
        items.Add(item);
        OnInventoryChanged();
    }
}
