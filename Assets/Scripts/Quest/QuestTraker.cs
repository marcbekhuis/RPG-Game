using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTraker : Singleton<QuestTraker>
{
    [SerializeField] private GameObject activeQuest;
    [SerializeField] private Transform activeQuestsParent;

    private Dictionary<Quest, trakingQuest> displayedQuests = new Dictionary<Quest, trakingQuest>();

    public void AddTrakingQuest(Quest quest)
    {
        GameObject placedActiveQuest = Instantiate(activeQuest, activeQuestsParent);
        Text questText = placedActiveQuest.GetComponentInChildren<Text>();
        displayedQuests.Add(quest, new trakingQuest(questText, placedActiveQuest));
        questText.text = quest.name + "\n" + quest.Objective();
    }

    public void UpdateTrakingQuest(Quest quest)
    {
        displayedQuests[quest].text.text = quest.name + "\n" + quest.Objective();
    }

    public void RemoveTrakingQuest(Quest quest)
    {
        Destroy(displayedQuests[quest].gameObject);
    }

    class trakingQuest 
    {
        public Text text;
        public GameObject gameObject;

        public trakingQuest(Text text, GameObject gameObject)
        {
            this.text = text;
            this.gameObject = gameObject;
        }
    }
}
