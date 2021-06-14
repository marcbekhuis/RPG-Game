using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogDisplay : Singleton<NPCDialogDisplay>
{
    [SerializeField] private GameObject display;
    [SerializeField] private Button tradeButton;

    public void DisplayDialog(Trading trading)
    {
        GameManager.instance.dialogOpen = true;
        display.SetActive(true);
        if (trading)
        {
            tradeButton.onClick.RemoveAllListeners();
            tradeButton.onClick.AddListener(() => TradingDisplay.instance.DisplayTrades(trading));
            tradeButton.gameObject.SetActive(true);
        }
        else tradeButton.gameObject.SetActive(false);
    }

    public void CloseDialog()
    {
        GameManager.instance.dialogOpen = false;
        display.SetActive(false);
    }
}
