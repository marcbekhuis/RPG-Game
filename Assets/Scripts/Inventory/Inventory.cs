using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int slots = 1;
    public Item[] startingItems;
    public List<Item> items;

    public delegate void InventoryChanged();
    public event InventoryChanged inventoryChanged = delegate { };

    // Start is called before the first frame update
    void Start()
    {
        AddStartingItems();
    }

    private void AddStartingItems()
    {
        foreach (var startingItem in startingItems)
        {
            AddItem(startingItem);
        }
    }

    public void ClearInventoryChanged()
    {
        inventoryChanged = delegate { };
    }

    public void AddItem(Item newItem)
    {
        int amountToAdd = newItem.amount;
        foreach (var item in items)
        {
            if (item.itemInformation != newItem.itemInformation) continue;
            if (item.amount < item.itemInformation.maxStackSize)
            {
                int stackAmountAvailable = item.itemInformation.maxStackSize - item.amount;
                int amountCantBeAdded = amountToAdd - stackAmountAvailable;
                int amountCanBeAdded = amountToAdd;
                if (amountCantBeAdded > 0) amountCanBeAdded = amountToAdd - amountCantBeAdded;
                amountToAdd = amountCantBeAdded;
                item.amount += amountCanBeAdded;
            }
        }
        if (amountToAdd <= 0) return;
        for (int i = amountToAdd; i > 0; i -= newItem.itemInformation.maxStackSize)
        {
            if (items.Count < slots)
            {
                if(i >= newItem.itemInformation.maxStackSize) items.Add(new Item(newItem.itemInformation, newItem.itemInformation.maxStackSize));
                else items.Add(new Item(newItem.itemInformation, i));
            }
        }
        inventoryChanged();
    }

    public void AddItem(Item newItem, int index)
    {
        items.Insert(index, newItem);
        inventoryChanged();
    }

    public void RemoveItemAtIndex(int index)
    {
        items.RemoveAt(index);
        inventoryChanged();
    }

    public void MoveItemInInventory(int currentIndex, int NewIndex)
    {
        Item movingItem = items[currentIndex];
        items.Remove(movingItem);
        if (NewIndex > items.Count - 1) items.Add(movingItem);
        else items.Insert(NewIndex, movingItem);
        Debug.LogError("Moved item");
        inventoryChanged();
    }
}
