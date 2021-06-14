using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradingDisplay : Singleton<TradingDisplay>
{
    [SerializeField] private GameObject tradeMenu;
    [SerializeField] private GameObject tradeItem;
    [SerializeField] private Transform tradeParent;
    [SerializeField] private InventoryDisplay inventoryDisplay;

    public void DisplayTrades(Trading trading)
    {
        inventoryDisplay.UpdateDisplay();

        foreach (Transform child in tradeParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var trade in trading.trades)
        {
            Instantiate(tradeItem, tradeParent);
        }

        tradeMenu.SetActive(true);
    }
}
