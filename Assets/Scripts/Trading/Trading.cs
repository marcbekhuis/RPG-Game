using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trading : MonoBehaviour
{
    public Trade[] possibleTrades;
    public List<Trade> trades;

    // Start is called before the first frame update
    void Start()
    {
        LoadTrades();
    }

    void LoadTrades()
    {
        foreach (var possibleTrade in possibleTrades)
        {
            Trade trade = new Trade(possibleTrade.change);
            if (Random.Range(0, 100) < trade.change)
            {
                foreach (var item in possibleTrade.costItems)
                {
                    trade.costItems.Add(new Trade.TradeItem(item, Random.Range(item.minAmount, item.maxAmount + 1)));
                }
                foreach (var item in possibleTrade.returnItems)
                {
                    trade.returnItems.Add(new Trade.TradeItem(item, Random.Range(item.minAmount, item.maxAmount + 1)));
                }
                trades.Add(trade);
            }
        }
    }
}
