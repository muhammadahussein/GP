using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quest3 : Quest
{
    public quest3()
    {
        QuestName = "Klaus";
        QuestDetails = "Find the key";
        ExpReward = 20;
        QuestOrder = 3;
        ItemReward = "Health Potion";
        Goals.Add(new ItemsGoal(this, "Key", "Find and retrieve the key", false, 0, 1));
        Goals.ForEach(goal => goal.Initialize());
    }
}
