using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Trade", menuName = "NPC/Trade")]
public class Trade : ScriptableObject
{
    public List<TradeItem> costItems;
    public List<TradeItem> returnItems;
    [Range(0, 100)] public float change = 50;

    [System.Serializable]
    public class TradeItem 
    {
        public Item item;
        [Min(1)] public int minAmount = 1;
        [Min(1)] public int maxAmount = 1;
        public int amount = 1;

        public TradeItem(TradeItem tradeItem)
        {
            this.item = tradeItem.item;
            this.minAmount = tradeItem.minAmount;
            this.maxAmount = tradeItem.maxAmount;
            this.amount = tradeItem.amount;
        }

        public TradeItem(TradeItem tradeItem, int amount)
        {
            this.item = tradeItem.item;
            this.minAmount = tradeItem.minAmount;
            this.maxAmount = tradeItem.maxAmount;
            this.amount = amount;
        }
    }

    public Trade(Trade trade)
    {
        this.costItems = trade.costItems;
        this.returnItems = trade.returnItems;
        this.change = trade.change;
    }

    public Trade(float change)
    {
        this.change = change;
        costItems = new List<TradeItem>();
        returnItems = new List<TradeItem>();
    }

    public void DoTrade(Inventory inventory)
    {
        foreach (var item in costItems)
        {
            inventory.RemoveItem(item.item, item.amount);
        }
        foreach (var item in returnItems)
        {
            inventory.AddItem(item.item, item.amount);
        }
    }
}
