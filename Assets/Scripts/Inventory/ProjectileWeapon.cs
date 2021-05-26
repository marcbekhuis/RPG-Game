using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New projectile weapon", menuName = "Items/Projectile weapon")]
public class ProjectileWeapon : Tool
{
    [Header("Projectile weapon")]
    [Min(0)] public int numberOfProjectiles = 1;
    [Range(0, 360)] public float arc = 90;
    public GameObject projectilePrefab;

    public ProjectileWeapon(ProjectileWeapon item, Inventory inventory) : base(item, inventory)
    {
        this.numberOfProjectiles = item.numberOfProjectiles;
        this.projectilePrefab = item.projectilePrefab;
        this.arc = item.arc;
    }

    public ProjectileWeapon(ProjectileWeapon item, Inventory inventory, int amount) : base(item, inventory, amount)
    {
        this.numberOfProjectiles = item.numberOfProjectiles;
        this.projectilePrefab = item.projectilePrefab;
        this.arc = item.arc;
    }

    public ProjectileWeapon() : base()
    {
        numberOfProjectiles = 2;
        arc = 90;
    }

    public override Item Create(Item item, Inventory inventory)
    {
        return new ProjectileWeapon((ProjectileWeapon)item, inventory);
    }

    public override Item Create(Item item, Inventory inventory, int amount)
    {
        return new ProjectileWeapon((ProjectileWeapon)item, inventory, amount);
    }

    // Replace "Void" with your interface of parent class for the attack.
    public override IAttack GetAttack(GameObject gameObject)
    {
        ProjectileAttack attack = gameObject.GetComponent<ProjectileAttack>();
        attack.SetSettings(arc, numberOfProjectiles, projectilePrefab);
        return attack;
    }

    public override string TooltipInformation()
    {
        string information = "<b>" + this.name + "</b>\n" + description + "\n\n" + "Number projectiles fired: " + numberOfProjectiles + "\n"
            + "Angle of attack: " + arc + "\n" + "Attack cooldown: " + attackCooldown + "\n" + "Movement cooldown: " + movementCooldown;
        if (maxStackSize > 1)
        {
            information += "\n\n" + "Amount: " + amount + "/" + maxStackSize;
        }
        return information;
    }
}
