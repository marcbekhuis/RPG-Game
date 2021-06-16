using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradingDisplay : Singleton<TradingDisplay>
{
    public GameObject tradeMenu;
    [SerializeField] private GameObject tradeItem;
    [SerializeField] private GameObject itemContainer;
    [SerializeField] private GameObject itemDisplay;
    [SerializeField] private Transform tradeParent;
    [SerializeField] private InventoryDisplay inventoryDisplay;

    public void DisplayTrades(Trading trading)
    {
        tradeMenu.SetActive(true);
        inventoryDisplay.UpdateDisplay();

        foreach (Transform child in tradeParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var trade in trading.trades)
        {
            bool canAfford = true;
            GameObject placedTrade = Instantiate(tradeItem, tradeParent);
            foreach (var item in trade.costItems)
            {
                GameObject placedItemContainer = Instantiate(itemContainer, placedTrade.transform.GetChild(0));
                GameObject placedItemDisplay = Instantiate(itemDisplay, placedItemContainer.transform);
                placedItemDisplay.transform.Find("Icon Image").GetComponent<Image>().sprite = item.item.icon;
                Text itemText = placedItemDisplay.transform.Find("Amount Text").GetComponent<Text>();
                itemText.text = inventoryDisplay.inventory.GetItemAmount(item.item).ToString() + "/" + item.amount.ToString();
                if (inventoryDisplay.inventory.HasItemAmount(item.item, item.amount)) itemText.color = Color.green;
                else
                {
                    canAfford = false;
                    itemText.color = Color.red;
                }
            }
            foreach (var item in trade.returnItems)
            {
                GameObject placedItemContainer = Instantiate(itemContainer, placedTrade.transform.GetChild(2));
                GameObject placedItemDisplay = Instantiate(itemDisplay, placedItemContainer.transform);
                placedItemDisplay.transform.Find("Icon Image").GetComponent<Image>().sprite = item.item.icon;
                placedItemDisplay.transform.Find("Amount Text").GetComponent<Text>().text = item.amount.ToString();
            }
            if (canAfford)
            {
                Button button = placedTrade.GetComponent<Button>();
                button.interactable = true;
                button.onClick.AddListener(() => trade.DoTrade(inventoryDisplay.inventory));
                button.onClick.AddListener(() => DisplayTrades(trading));
            }
            else
            {
                placedTrade.GetComponent<Button>().interactable = false;
            }
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(tradeParent.GetComponent<RectTransform>());
    }
}
