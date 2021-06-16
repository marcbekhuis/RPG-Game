using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestingDisplay : Singleton<QuestingDisplay>
{
    public GameObject questingMenu;
    [SerializeField] private GameObject questItem;
    [SerializeField] private Transform questParent;
    [SerializeField] private Inventory inventory;

    public void DisplayQuest(Questing questing)
    {
        questingMenu.SetActive(true);

        foreach (Transform child in questParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var quest in questing.quests)
        {
            GameObject placedQuest = Instantiate(questItem, questParent);
            placedQuest.transform.Find("Story Text").GetComponent<Text>().text = quest.story;
            placedQuest.transform.Find("Objective Text").GetComponent<Text>().text = quest.Objective();
            if (quest.completed)
            {
                placedQuest.transform.Find("Button").gameObject.SetActive(false);
                placedQuest.transform.Find("Status Text").GetComponent<Text>().text = "Completed";
            }
            else if (quest.RewardClaimable)
            {
                Button button = placedQuest.transform.Find("Button").GetComponent<Button>();
                button.GetComponentInChildren<Text>().text = "Claim reward";
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => quest.ClaimReward(inventory));
                button.onClick.AddListener(() => DisplayQuest(questing));
                placedQuest.transform.Find("Status Text").GetComponent<Text>().text = "Reward claimable";
            }
            else if (quest.started)
            {
                placedQuest.transform.Find("Button").gameObject.SetActive(false);
                placedQuest.transform.Find("Status Text").GetComponent<Text>().text = "Started";
            }
            else
            {
                Button button = placedQuest.transform.Find("Button").GetComponent<Button>();
                button.GetComponentInChildren<Text>().text = "start";
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => quest.StartQuest());
                button.onClick.AddListener(() => DisplayQuest(questing));
                placedQuest.transform.Find("Status Text").GetComponent<Text>().text = "Not yet started";
            }
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(questParent.GetComponent<RectTransform>());
    }
}
