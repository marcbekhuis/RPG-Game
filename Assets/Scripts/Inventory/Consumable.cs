using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : Item
{
    [Header("Consumable")]
    [Min(0)] public float cooldown = 1;
    [Min(0)] public float movementCooldown = 0.1f;

    // Replace "Void" with your interface of parent class for the Consume.
    public abstract IConsume GetConsume(GameObject gameObject);

    public Consumable() : base()
    {
        cooldown = 1;
        movementCooldown = 0.1f;
    }

    public Consumable(Consumable item, Inventory inventory, int amount) : base(item, inventory, amount)
    {
        cooldown = item.cooldown;
        movementCooldown = item.movementCooldown;
    }

    public Consumable(Consumable item, Inventory inventory) : base(item, inventory)
    {
        cooldown = item.cooldown;
        movementCooldown = item.movementCooldown;
    }
}
