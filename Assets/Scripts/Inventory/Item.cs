using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName = "Items/Item")]
public class Item : ScriptableObject
{
    [Header("Item")]
    public Sprite icon;
    public string description = "A basic item.";
    [Min(1)] public int maxStackSize = 10;

    [HideInInspector] public int amount = 0;
    [HideInInspector] public Inventory inventory;

    public Item(Item item, Inventory inventory)
    {
        this.inventory = inventory;
        this.icon = item.icon;
        this.description = item.description;
        this.amount = item.amount;
        this.maxStackSize = item.maxStackSize;
        this.name = item.name;
    }

    public Item(Item item, Inventory inventory, int amount)
    {
        this.inventory = inventory;
        this.icon = item.icon;
        this.description = item.description;
        this.amount = amount;
        this.maxStackSize = item.maxStackSize;
        this.name = item.name;
    }

    public Item()
    {
        description = "A basic item.";
        maxStackSize = 10;
        amount = 0;
    }

    public virtual Item Create(Item item, Inventory inventory)
    {
        return new Item(item, inventory);
    }

    public virtual Item Create(Item item, Inventory inventory, int amount)
    {
        return new Item(item, inventory, amount);
    }

    public virtual string TooltipInformation()
    {
        string information = "<b>" + this.name + "</b>\n" + description;
        if(maxStackSize > 1)
        {
            information += "\n\n" + "Amount: " + amount + "/" + maxStackSize;
        }
        return information;
    }
}
