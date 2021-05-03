using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private Transform inventory;
    [SerializeField] private GameObject itemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        playerInventory.inventoryChanged += UpdateInventory;
    }

    public void UpdateInventory()
    {
        foreach (Transform child in inventory)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in playerInventory.items)
        {
            GameObject newItem = Instantiate(itemPrefab, inventory);
            newItem.transform.GetChild(0).GetComponent<Image>().sprite = item.itemInformation.sprite;
            newItem.transform.GetChild(1).GetComponent<Text>().text = item.amount.ToString();
        }
    }
}
