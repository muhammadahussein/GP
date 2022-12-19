using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Quest : MonoBehaviour
{
    public List<QuestGoal> Goals { get; set; } = new List<QuestGoal>();
    public string QuestName { get; set; }
    public string QuestDetails { get; set; }
    public int ExpReward { get; set; }
    public Item ItemReward { get; set; }
    public bool QuestCompleted { get; set; }

    public void CheckIfCompleted()
    {
        QuestCompleted = Goals.All(goal => goal.GoalCompleted);
    }

    public void GiveReward()
    {
        if(ItemReward!=null)
            InventoryHandler.Instance.AddItem(ItemReward);
    }
}
