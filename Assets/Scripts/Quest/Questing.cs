using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Questing : MonoBehaviour
{
    public Quest[] possibleQuests;
    public List<Quest> quests;

    // Start is called before the first frame update
    void Start()
    {
        LoadQuests();
    }

    void LoadQuests()
    {
        foreach (var possibleQuest in possibleQuests)
        {
            Quest quest = possibleQuest.Create(possibleQuest);
            quests.Add(quest);
        }
    }
}
