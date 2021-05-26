using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : Item
{
    [Header("Tool")]
    [Min(0)] public float attackCooldown = 1;
    [Min(0)] public float movementCooldown = 0.1f;

    public Tool() : base()
    {
        attackCooldown = 1;
        movementCooldown = 0.1f;
    }

    public Tool(Tool item, Inventory inventory, int amount) : base(item, inventory, amount)
    {
        attackCooldown = item.attackCooldown;
        movementCooldown = item.movementCooldown;
    }

    public Tool(Tool item, Inventory inventory) : base(item, inventory)
    {
        attackCooldown = item.attackCooldown;
        movementCooldown = item.movementCooldown;
    }

    // Replace "Void" with your interface of parent class for the attack.
    public abstract IAttack GetAttack(GameObject gameObject);
}
