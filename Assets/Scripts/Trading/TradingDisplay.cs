using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradingDisplay : MonoBehaviour
{
    [SerializeField] private Trading trading;
    [SerializeField] private GameObject tradeItem;
    [SerializeField] private Transform tradeParent;

    public void DisplayTrades()
    {
        foreach (Transform child in tradeParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var trade in trading.trades)
        {
            Instantiate(tradeItem, tradeParent);
        }
    }
}
