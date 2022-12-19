using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFiveQuest : Quest
{
    void Start()
    {
        QuestName = "Kill Hima";
        QuestDetails = "Kill five enemies";
        ExpReward = 20;
        Goals.Add(new KillGoal(this, 0, "Kill five enemies", false, 0, 5));
        Goals.Add(new ItemsGoal(this, "Potion", "Collect 2 potions", false, 0, 2));
        Goals.ForEach(goal => goal.Initialize());
    }
}
