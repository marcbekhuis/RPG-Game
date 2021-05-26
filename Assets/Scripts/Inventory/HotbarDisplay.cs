using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarDisplay : InventoryDisplay
{
    [SerializeField] private InputActions[] slotSelectActions;
    [SerializeField] private PlayerController playerController;
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
        itemContainer.SetBackgroundColor(selectColor);
    }

    private void RemoveItemUsage()
    {
        if (!selectedItemContainer) return;
        if (!selectedItemContainer.item) return;
        if (selectedItemContainer.item.GetType().BaseType == typeof(Tool))
        {
            playerController.attack -= ((Tool)selectedItemContainer.item).GetAttack(playerController.gameObject).AttackCall;
        }
    }

    private void AddItemUsage()
    {
        if (!selectedItemContainer) return;
        if (!selectedItemContainer.item) return;
        if (selectedItemContainer.item.GetType().BaseType == typeof(Tool))
        {
            Tool tool = (Tool)selectedItemContainer.item;
            playerController.attack += tool.GetAttack(playerController.gameObject).AttackCall;
            playerController.SetCooldowns(tool.attackCooldown, tool.movementCooldown);
        }
    }
}
