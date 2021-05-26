using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : Item
{
    // Replace "Void" with your interface of parent class for the Consume.
    public abstract IConsume GetConsume(GameObject gameObject);

    public Consumable() : base()
    {
    }

    public Consumable(Consumable item, Inventory inventory, int amount) : base(item, inventory, amount)
    {
    }

    public Consumable(Consumable item, Inventory inventory) : base(item, inventory)
    {
    }
}
