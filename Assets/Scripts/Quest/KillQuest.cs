using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Kill Quest", menuName = "Quest/Kill Quest")]
public class KillQuest : Quest
{
    public KillCreature[] killCreatures;

    public KillQuest(KillQuest quest) : base(quest)
    {
        this.killCreatures = new KillCreature[quest.killCreatures.Length];
        for (int i = 0; i < this.killCreatures.Length; i++)
        {
            this.killCreatures[i] = new KillCreature(quest.killCreatures[i]);
        }
        AIDeath.OnStaticDead += OnCreatureDead;
    }

    public override Quest Create(Quest quest)
    {
        return new KillQuest((KillQuest)quest);
    }

    public override string Objective()
    {
        string objective = "";
        for (int i = 0; i < killCreatures.Length; i++)
        {
            objective += killCreatures[0].creatureName + " " + killCreatures[0].amount.ToString() + "/" + killCreatures[0].neededAmount.ToString();
            if (i < killCreatures.Length - 1) objective += "\n";
        }
        return objective;
    }

    public void OnCreatureDead(string creatureName)
    {
        if (!started) return;
        foreach (var killCreature in killCreatures)
        {
            if(killCreature.creatureName.ToLower() == creatureName.ToLower())
            {
                killCreature.amount++;
                if (killCreature.amount >= killCreature.neededAmount) RewardClaimable = true;
            }
        }
        QuestTraker.instance.UpdateTrakingQuest(this);
    }

    [System.Serializable]
    public class KillCreature 
    {
        public string creatureName;
        [Min(1)] public int minNeededAmount = 1;
        [Min(1)] public int maxNeededAmount = 1;
        [HideInInspector] public int neededAmount = 1;
        [HideInInspector] public int amount = 0;

        public KillCreature(KillCreature killCreature)
        {
            this.creatureName = killCreature.creatureName;
            this.neededAmount = Random.Range(killCreature.minNeededAmount, killCreature.maxNeededAmount + 1);
            this.amount = 0;
        }
    }
}
