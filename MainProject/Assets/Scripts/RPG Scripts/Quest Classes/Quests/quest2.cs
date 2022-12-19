using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quest2 : Quest
{
    public quest2()
    {
        QuestName = "Hima";
        QuestDetails = "Kill Hima";
        ExpReward = 20;
        QuestOrder = 2;
        ItemReward = "Apple";
        Goals.Add(new KillGoal(this, 0, "Collect Gumarich's wife's ring", false, 0, 1));
        Goals.ForEach(goal => goal.Initialize());
    }
}
