using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillGoal : QuestGoal
{
    public int EnemyID { get; set; } //each enemy is gonna have an id property (Enemy Interface)

    #region PUBLIC API

    public KillGoal(Quest quest, int enemyId, string details, bool isCompleted, int questProgress, int requiredProgress)
    {
        this.Quest = quest;
        this.EnemyID = enemyId;
        this.QuestDetails = details;
        this.GoalCompleted = isCompleted;
        this.GoalProgress = questProgress;
        this.RequiredProgress = requiredProgress;
    }

    public override void Initialize()
    {
        base.Initialize();
        //OnEnemyKill Event trigger
    }
    #endregion

    #region PRIVATE API

    void EnemyDied()
    {
        //if(enemy.ID == this.EnemyID)
        this.GoalProgress++;
        CheckIfComplete();
    }

    #endregion
}
