using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogDisplay : Singleton<NPCDialogDisplay>
{
    [SerializeField] private GameObject display;
    [SerializeField] private Button tradeButton;
    [SerializeField] private Button questButton;

    public void DisplayDialog(Trading trading, Questing questing)
    {
        GameManager.instance.dialogOpen = true;
        display.SetActive(true);
        if (trading.trades.Count > 0)
        {
            tradeButton.onClick.RemoveAllListeners();
            tradeButton.onClick.AddListener(() => TradingDisplay.instance.DisplayTrades(trading));
            tradeButton.onClick.AddListener(() => display.SetActive(false));
            Button button = TradingDisplay.instance.tradeMenu.transform.Find("Close Button").GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => TradingDisplay.instance.tradeMenu.SetActive(false));
            button.onClick.AddListener(() => DisplayDialog(trading, questing));
            tradeButton.gameObject.SetActive(true);
        }
        else tradeButton.gameObject.SetActive(false);
        if (questing.quests.Count > 0)
        {
            questButton.onClick.RemoveAllListeners();
            questButton.onClick.AddListener(() => QuestingDisplay.instance.DisplayQuest(questing));
            questButton.onClick.AddListener(() => display.SetActive(false));
            Button button = QuestingDisplay.instance.questingMenu.transform.Find("Close Button").GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => QuestingDisplay.instance.questingMenu.SetActive(false));
            button.onClick.AddListener(() => DisplayDialog(trading, questing));
            questButton.gameObject.SetActive(true);
        }
        else questButton.gameObject.SetActive(false);
    }

    public void CloseDialog()
    {
        GameManager.instance.dialogOpen = false;
        display.SetActive(false);
    }
}
