using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private GameObject fullInventory;
    [SerializeField] private GameObject secondInventory;
    [SerializeField] private InventoryDisplay playerInventoryDisplay;
    [SerializeField] private InventoryDisplay playerInventoryDisplay2;
    [SerializeField] private InventoryDisplay OtherInventoryDisplay;

    public void ToggleInventory(Inventory otherInventory)
    {
        if (otherInventory)
        {
            secondInventory.SetActive(!secondInventory.activeSelf);
            fullInventory.SetActive(false);
            GameManager.instance.inventoryOpen = secondInventory.activeSelf;

            OtherInventoryDisplay.inventory = otherInventory;
            OtherInventoryDisplay.UpdateInventory();
            playerInventoryDisplay2.UpdateInventory();
        }
        else
        {
            fullInventory.SetActive(!fullInventory.activeSelf);
            secondInventory.SetActive(false);
            GameManager.instance.inventoryOpen = fullInventory.activeSelf;

            playerInventoryDisplay.UpdateInventory();
        }
    }
}
