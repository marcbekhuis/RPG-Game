using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantHealingSystem : MonoBehaviour, IConsume
{
    [SerializeField] private HealthSystem healthSystem;

    private float healthAdd = 0;
    private Item item;

    public void Consume()
    {
        healthSystem.AddHealth(healthAdd);
        item.inventory.RemoveItem(item, 1);
    }

    public void SetSettings(float healthAdd, Item item)
    {
        this.healthAdd = healthAdd;
        this.item = item;
    }
}
