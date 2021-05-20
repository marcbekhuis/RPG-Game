using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarDisplay : MonoBehaviour
{
    [SerializeField] public Inventory inventory;
    [SerializeField] public Transform[] hotbarSlots;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Color slotEvenColor;
    [SerializeField] private Color slotUnevenColor;
    [SerializeField] private Color slotSelectedColor;

    // Start is called before the first frame update
    void Awake()
    {
        inventory.inventoryChanged += UpdateInventory;
    }

    private void Update()
    {
        if (InputManager.instance.GetKeyDown(InputActions.HotbarSlot1)) SelectSlot(1);
        if (InputManager.instance.GetKeyDown(InputActions.HotbarSlot2)) SelectSlot(2);
        if (InputManager.instance.GetKeyDown(InputActions.HotbarSlot3)) SelectSlot(3);
        if (InputManager.instance.GetKeyDown(InputActions.HotbarSlot4)) SelectSlot(4);
        if (InputManager.instance.GetKeyDown(InputActions.HotbarSlot5)) SelectSlot(5);
        if (InputManager.instance.GetKeyDown(InputActions.HotbarSlot6)) SelectSlot(6);
        if (InputManager.instance.GetKeyDown(InputActions.HotbarSlot7)) SelectSlot(7);
        if (InputManager.instance.GetKeyDown(InputActions.HotbarSlot8)) SelectSlot(8);
        if (InputManager.instance.GetKeyDown(InputActions.HotbarSlot9)) SelectSlot(9);
    }

    public void UpdateInventory()
    {
        foreach (var hotbarslot in hotbarSlots)
        {
            if(hotbarslot.childCount > 0) Destroy(hotbarslot.GetChild(0).gameObject);
        }

        if (!GameManager.instance.inventoryOpen) return;

        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            GameObject newItem = Instantiate(itemPrefab, hotbarSlots[i]);
            newItem.transform.GetChild(0).GetComponent<Image>().sprite = inventory.items[i].itemInformation.sprite;
            if (inventory.items[i].itemInformation.maxStackSize <= 1) newItem.transform.GetChild(1).gameObject.SetActive(false);
            else newItem.transform.GetChild(1).GetComponent<Text>().text = inventory.items[i].amount.ToString();
        }
    }

    public void SelectSlot(int index)
    {
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            if (i == index) hotbarSlots[i].GetComponent<Image>().color = slotSelectedColor;
            else
            {
                if((i + 1) % 2f == 0.5f) hotbarSlots[i].GetComponent<Image>().color = slotUnevenColor;
                else hotbarSlots[i].GetComponent<Image>().color = slotEvenColor;
            }
        }
    }
}
