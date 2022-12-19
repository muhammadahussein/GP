using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : NPC
{
    public bool AssignedQuest { get; set; }
    public bool CompletedQuest { get; set; }
    private Quest Quest { get; set; }
    
    [SerializeField] private GameObject quests;
    [SerializeField] private string questName;
    public override void Interact()
    {
        base.Interact();
        if (!AssignedQuest & !CompletedQuest)
        {
            AssignQuest();
        }
        else if (AssignedQuest && !CompletedQuest)
        {
            CheckQuest();
        }
        else
        {
            DialogueSystem.Instance.NewDialogue(afterRewardDialouge,  npcName);
        }
    }

    void AssignQuest()
    {
        AssignedQuest = true;
        Quest = (Quest)quests.AddComponent(System.Type.GetType(questName));
    }

    void CheckQuest()
    {
        if (Quest.QuestCompleted)
        {
            Quest.GiveReward();
            CompletedQuest = true;
            AssignedQuest = false;
            DialogueSystem.Instance.NewDialogue(rewardDialouge,  npcName);
        }
        else
        {
            DialogueSystem.Instance.NewDialogue(inProgressDialogue,  npcName);
        }
    }
}
