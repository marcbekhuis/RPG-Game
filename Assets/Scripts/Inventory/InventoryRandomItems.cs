using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryRandomItems : MonoBehaviour
{
    [SerializeField] private Entry[] randomItems;
    [SerializeField] private Inventory inventory;

    [System.Serializable]
    public class Entry 
    {
        public Item item;
        [Min(0)] public int minAmount = 0;
        [Min(0)] public int maxAmount = 1;
    }

    public void AddItems()
    {
        foreach (var randomItem in randomItems)
        {
            inventory.AddItem(randomItem.item, Random.Range(randomItem.minAmount, randomItem.maxAmount + 1));
        }
    }
}
