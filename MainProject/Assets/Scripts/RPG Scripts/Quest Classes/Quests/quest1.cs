using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quest1 : Quest
{
    public quest1()
    {
        QuestName = "Gumarich";
        QuestDetails = "Find Gumarich's ring";
        ExpReward = 20;
        QuestOrder = 1;
        ItemReward = "Health Potion";
        Goals.Add(new ItemsGoal(this, "Ring", "Collect Gumarich's wife's ring", false, 0, 1));
        Goals.ForEach(goal => goal.Initialize());
    }
}
