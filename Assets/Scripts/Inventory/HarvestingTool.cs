using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New harvesting tool", menuName = "Items/Harvesting tool")]
public class HarvestingTool : Tool
{
    [Header("Harvesting Tool")]
    [Min(0)] public float range = 1;
    [Range(0, 360)] public float arc = 90;

    public HarvestingTool(HarvestingTool item, Inventory inventory) : base(item, inventory)
    {
        this.range = item.range;
        this.arc = item.arc;
    }

    public HarvestingTool(HarvestingTool item, Inventory inventory, int amount) : base(item, inventory, amount)
    {
        this.range = item.range;
        this.arc = item.arc;
    }

    public HarvestingTool() : base()
    {
        range = 1;
        arc = 90;
    }

    public override Item Create(Item item, Inventory inventory)
    {
        return new HarvestingTool((HarvestingTool)item, inventory);
    }

    public override Item Create(Item item, Inventory inventory, int amount)
    {
        return new HarvestingTool((HarvestingTool)item, inventory, amount);
    }

    // Replace "Void" with your interface of parent class for the attack.
    public override IAttack GetAttack(GameObject gameObject)
    {
        HarvestingAttack attack = gameObject.GetComponent<HarvestingAttack>();
        attack.SetSettings(arc, range);
        return attack;
    }

    public override string TooltipInformation()
    {
        string information = "<b>" + this.name + "</b>\n" + description + "\n\n" + "Range: " + range + "\n"
            + "Angle of harvest: " + arc + "\n" + "Attack cooldown: " + attackCooldown + "\n" + "Movement cooldown: " + movementCooldown;
        if (maxStackSize > 1)
        {
            information += "\n\n" + "Amount: " + amount + "/" + maxStackSize;
        }
        return information;
    }
}
