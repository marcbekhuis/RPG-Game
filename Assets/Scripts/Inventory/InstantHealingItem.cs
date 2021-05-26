using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New healing item", menuName = "Items/Instant healing item")]
public class InstantHealingItem : Consumable
{
    [Header("Healing item")]
    public float healing;

    public InstantHealingItem(InstantHealingItem item, Inventory inventory) : base(item, inventory)
    {
        this.healing = item.healing;
    }

    public InstantHealingItem(InstantHealingItem item, Inventory inventory, int amount) : base(item, inventory, amount)
    {
        this.healing = item.healing;
    }

    public InstantHealingItem() : base()
    {
        healing = 1;
    }

    public override Item Create(Item item, Inventory inventory)
    {
        return new InstantHealingItem((InstantHealingItem)item, inventory);
    }

    public override Item Create(Item item, Inventory inventory, int amount)
    {
        return new InstantHealingItem((InstantHealingItem)item, inventory, amount);
    }

    public override IConsume GetConsume(GameObject gameObject)
    {
        InstantHealingSystem instantHealingSystem = gameObject.GetComponent<InstantHealingSystem>();
        instantHealingSystem.SetSettings(healing, this);
        return instantHealingSystem;
    }

    public override string TooltipInformation()
    {
        string information = "<b>" + this.name + "</b>\n" + description + "\n\n" + "Heals: " + healing + " HP";
        if (maxStackSize > 1)
        {
            information += "\n\n" + "Amount: " + amount + "/" + maxStackSize;
        }
        return information;
    }
}
