using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGoal
{
    public Quest Quest { get; set; }
    public string QuestDetails { get; set; }
    public bool GoalCompleted { get; set; }
    public int GoalProgress { get; set; }
    public int RequiredProgress { get; set; }

    #region PUBLIC API

    public void CheckIfComplete()
    {
        if(GoalProgress >= RequiredProgress)
            CompleteQuest();
    }

    public void CompleteQuest()
    {
        GoalCompleted = true;
        Quest.CheckIfCompleted();
    }

    public virtual void Initialize()
    {
        
    }
    #endregion
}