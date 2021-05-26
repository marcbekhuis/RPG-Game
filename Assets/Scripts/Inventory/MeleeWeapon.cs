using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New melee weapon", menuName = "Items/Melee weapon")]
public class MeleeWeapon : Tool
{
    [Header("Melee weapon")]
    [Min(0)] public float damage = 1;
    [Min(0)] public float range = 1;
    [Range(0,360)] public float arc = 90;

    public MeleeWeapon(MeleeWeapon item, Inventory inventory) : base(item, inventory)
    {
        this.damage = item.damage;
        this.range = item.range;
        this.arc = item.arc;
    }

    public MeleeWeapon(MeleeWeapon item, Inventory inventory, int amount) : base(item, inventory, amount)
    {
        this.damage = item.damage;
        this.range = item.range;
        this.arc = item.arc;
    }

    public MeleeWeapon() : base()
    {
        damage = 2;
        range = 1;
        arc = 90;
    }

    public override Item Create(Item item, Inventory inventory)
    {
        return new MeleeWeapon((MeleeWeapon)item, inventory);
    }

    public override Item Create(Item item, Inventory inventory, int amount)
    {
        return new MeleeWeapon((MeleeWeapon)item, inventory, amount);
    }

    // Replace "Void" with your interface of parent class for the attack.
    public override IAttack GetAttack(GameObject gameObject)
    {
        MeleeAttack attack = gameObject.GetComponent<MeleeAttack>();
        attack.SetSettings(damage, arc, range);
        return attack;
    }

    public override string TooltipInformation()
    {
        string information = "<b>" + this.name + "</b>\n" + description + "\n\n" + "Damage: " + damage + "\n" + "Range: " + range + "\n"
            + "Angle of attack: " + arc + "\n" + "Attack cooldown: " + attackCooldown + "\n" + "Movement cooldown: " + movementCooldown;
        if (maxStackSize > 1)
        {
            information += "\n\n" + "Amount: " + amount + "/" + maxStackSize;
        }
        return information;
    }
}
