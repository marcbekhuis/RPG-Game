using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] public Inventory inventory;
    [SerializeField] public Transform inventoryHolder;
    [SerializeField] private GameObject itemPrefab;

    public Inventory Inventory 
    {
        get { return inventory; }
        set {
            if(inventory) inventory.ClearInventoryChanged();
            inventory = value;
            inventory.inventoryChanged += UpdateInventory;
        }
    }


    // Start is called before the first frame update
    void Awake()
    {
        if(inventory)inventory.inventoryChanged += UpdateInventory;
    }

    public void UpdateInventory()
    {
        foreach (Transform child in inventoryHolder)
        {
            Destroy(child.gameObject);
        }

        if (!GameManager.instance.inventoryOpen) return;

        foreach (var item in inventory.items)
        {
            GameObject newItem = Instantiate(itemPrefab, inventoryHolder);
            newItem.transform.GetChild(0).GetComponent<Image>().sprite = item.itemInformation.sprite;
            if (item.itemInformation.maxStackSize <= 1) newItem.transform.GetChild(1).gameObject.SetActive(false);
            else newItem.transform.GetChild(1).GetComponent<Text>().text = item.amount.ToString();
        }
    }
}
