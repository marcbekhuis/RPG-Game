using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarDisplay : InventoryDisplay
{
    [SerializeField] private InputActions[] slotSelectActions;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private SpriteRenderer handHeldItem;
    [Header("Customization")]
    [SerializeField] private Color selectColor = Color.red;
    [SerializeField] private Color defaultColor = Color.white;

    private ItemContainer selectedItemContainer;

    void Update()
    {
        if (GameManager.instance.GamePaused()) return;
        for (int i = 0; i < slotSelectActions.Length; i++)
        {
            if (InputManager.instance.GetKeyDown(slotSelectActions[i]))
            {
                if (itemContainers.Count < i) return;
                ItemContainer itemContainer = itemContainers[i];
                SelectSlot(itemContainer);
                return;
            }
        }
    }

    public override void UpdateDisplay()
    {
        RemoveItemUsage();
        base.UpdateDisplay();
        AddItemUsage();
    }

    private void SelectSlot(ItemContainer itemContainer)
    {
        if (selectedItemContainer)
        {
            selectedItemContainer.SetBackgroundColor(defaultColor);
            if (selectedItemContainer.item)
            {
                if (selectedItemContainer.item.GetType().BaseType == typeof(Tool))
                {
                    playerController.attack -= ((Tool)selectedItemContainer.item).GetAttack(playerController.gameObject).AttackCall;
                }
                else if (selectedItemContainer.item.GetType().BaseType == typeof(Consumable))
                {
                    playerController.consume -= ((Consumable)selectedItemContainer.item).GetConsume(playerController.gameObject).Consume;
                }
            }
        }
        selectedItemContainer = itemContainer;
        if (selectedItemContainer.item)
        {
            handHeldItem.sprite = selectedItemContainer.item.icon;
            if (selectedItemContainer.item.GetType().BaseType == typeof(Tool))
            {
                Tool tool = (Tool)selectedItemContainer.item;
                playerController.attack += tool.GetAttack(playerController.gameObject).AttackCall;
                playerController.SetCooldowns(tool.attackCooldown, tool.movementCooldown);
            }
            else if (selectedItemContainer.item.GetType().BaseType == typeof(Consumable))
            {
                Consumable consumable = (Consumable)selectedItemContainer.item;
                playerController.consume += consumable.GetConsume(playerController.gameObject).Consume;
                playerController.SetCooldowns(consumable.cooldown, consumable.movementCooldown);
            }
        }
        else handHeldItem.sprite = null;
        itemContainer.SetBackgroundColor(selectColor);
    }

    public void RemoveItemUsage()
    {
        if (!selectedItemContainer) return;
        if (!selectedItemContainer.item) return;
        handHeldItem.sprite = null;
        if (selectedItemContainer.item.GetType().BaseType == typeof(Tool))
        {
            playerController.attack -= ((Tool)selectedItemContainer.item).GetAttack(playerController.gameObject).AttackCall;
        }
        else if (selectedItemContainer.item.GetType().BaseType == typeof(Consumable))
        {
            playerController.consume -= ((Consumable)selectedItemContainer.item).GetConsume(playerController.gameObject).Consume;
        }
    }

    public void AddItemUsage()
    {
        if (!selectedItemContainer) return;
        if (!selectedItemContainer.item) return;
        handHeldItem.sprite = selectedItemContainer.item.icon;
        if (selectedItemContainer.item.GetType().BaseType == typeof(Tool))
        {
            Tool tool = (Tool)selectedItemContainer.item;
            playerController.attack += tool.GetAttack(playerController.gameObject).AttackCall;
            playerController.SetCooldowns(tool.attackCooldown, tool.movementCooldown);
        }
        else if (selectedItemContainer.item.GetType().BaseType == typeof(Consumable))
        {
            playerController.consume += ((Consumable)selectedItemContainer.item).GetConsume(playerController.gameObject).Consume;
        }
    }
}
