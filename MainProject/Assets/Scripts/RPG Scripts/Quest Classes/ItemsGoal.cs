using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsGoal : QuestGoal
{
    public string ItemID { get; set; } //each enemy is gonna have an id property (Enemy Interface)

    #region PUBLIC API

    public ItemsGoal(Quest quest, string itemId, string details, bool isCompleted, int questProgress, int requiredProgress)
    {
        this.Quest = quest;
        this.ItemID = itemId;
        this.QuestDetails = details;
        this.GoalCompleted = isCompleted;
        this.GoalProgress = questProgress;
        this.RequiredProgress = requiredProgress;
    }

    public override void Initialize()
    {
        base.Initialize();
        UIEventManager.OnItemAdded += ItemPicked;
    }
    #endregion

    #region PRIVATE API

    void ItemPicked(Item item)
    {
        if(item.ItemName == this.ItemID)
        {
            this.GoalProgress++;
            CheckIfComplete();
        }
    }

    #endregion
}
