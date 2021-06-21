using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapDisplay : MonoBehaviour
{
    [SerializeField] private GameObject display;
    [SerializeField] private Item[] neededItems;
    [SerializeField] private Inventory[] inventories;

    // Update is called once per frame
    void Update()
    {
        if (InputManager.instance.GetKeyDown(InputActions.Minimap))
        {
            bool hasItems = true;
            foreach (var item in neededItems)
            {
                bool anyInventoryHasItem = false;
                foreach (var inventory in inventories)
                {
                    anyInventoryHasItem = inventory.HasItem(item);
                    if (anyInventoryHasItem) break;
                }
                if (!anyInventoryHasItem) hasItems = false;
            }
            if(hasItems) display.SetActive(!display.activeSelf);
            else display.SetActive(false);
        }
    }
}
