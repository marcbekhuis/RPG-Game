using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    [HideInInspector] public Item item;
    [HideInInspector] public ItemContainer itemContainer;
    [HideInInspector] public Inventory inventory;
    [HideInInspector] public InventoryDisplay inventoryDisplay;

    [SerializeField] private Text amountText;
    [SerializeField] private Image iconImage;

    private RectTransform rectTransform;
    private Vector3 containerPosition;
    private Canvas canvas;

    public void Setup(Item item, ItemContainer itemContainer, Inventory inventory, InventoryDisplay inventoryDisplay)
    {
        this.itemContainer = itemContainer;
        this.inventory = inventory;
        this.inventoryDisplay = inventoryDisplay;
        rectTransform = GetComponent<RectTransform>();
        containerPosition = rectTransform.anchoredPosition;
        SetItem(item);
    }

    public void SetItem(Item item)
    {
        this.item = item;
        if (!this.item) return;
        rectTransform.anchoredPosition = containerPosition;
        amountText.text = this.item.amount.ToString();
        iconImage.sprite = item.icon;  
        if(this.item.maxStackSize <= 1)
            amountText.gameObject.SetActive(false);
        else
            amountText.gameObject.SetActive(true);
    }

    public void SetPosition(Vector2 position)
    {
        rectTransform.position = position;
    }

    public void AddCanvas()
    {
        if (canvas) return;
        canvas = this.gameObject.AddComponent<Canvas>();
        canvas.overrideSorting = true;
        canvas.sortingOrder = 10;
    }

    public void RemoveCanvas()
    {
        if (!canvas) return;
        Destroy(canvas);
        canvas = null;
    }
}
