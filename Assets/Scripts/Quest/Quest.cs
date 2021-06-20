using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Quest : ScriptableObject
{
    public string story;
    public Reward[] Rewards;
    [Range(0, 100)] public float change = 100;
    [HideInInspector] public bool RewardClaimable = false;
    [HideInInspector] public bool started = false;
    [HideInInspector] public bool completed = false;

    [System.Serializable]
    public class Reward
    {
        public Item item;
        [Min(1)] public int amount;
    }

    public virtual void StartQuest()
    {
        started = true;
    }

    public abstract string Objective();

    public virtual void ClaimReward(Inventory inventory)
    {
        completed = true;
        foreach (var questReward in Rewards)
        {
            inventory.AddItem(questReward.item, questReward.amount);
        }
    }

    public Quest(Quest quest)
    {
        this.name = quest.name;
        this.story = quest.story;
        this.Rewards = quest.Rewards;
        this.RewardClaimable = false;
        this.completed = false;
        this.started = false;
    }

    public abstract Quest Create(Quest quest);
}
