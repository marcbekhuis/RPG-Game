using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Item
{
    public ItemInformation itemInformation;
    public int amount = 0;

    public Item(ItemInformation itemInformation, int amount)
    {
        this.itemInformation = itemInformation;
        this.amount = amount;
    }
}
