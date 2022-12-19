using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class QuestSave
{

    public int questOrder;
    public bool questState;
    public string questName;
    public QuestSave(int qO, bool qS, string qN)
    {
        questOrder = qO;
        questState = qS;
        questName = qN;
    }
}

[System.Serializable]
public class QuestGiver : NPC
{
    public bool AssignedQuest { get; set; }
    public bool CompletedQuest { get; set; }
    public Quest Quest { get; set; }
    
    [SerializeField] private GameObject quests;
    [SerializeField] public string questName;
    [SerializeField] protected GameObject questStart, questEnd;
    [HideInInspector]public Vector3 SpawnPoint;
    [SerializeField] int QuestOrder;

    QuestSave questSave;
    public override void Interact()
    {
        base.Interact();
        
        if (!AssignedQuest & !CompletedQuest)
        {
            AssignQuest(false);
        }
        else if (AssignedQuest && !CompletedQuest)
        {
            CheckQuest(false);
        }
        else
        {
            DialogueSystem.Instance.NewDialogue(afterRewardDialouge,  npcName);
        }
    }
    public void LoadQuestState()
    {
        
    }

    public void AssignQuest(bool isLoading)
    {
        AssignedQuest = true;
        Quest = (Quest)quests.AddComponent(System.Type.GetType(questName));
        if(questStart)questStart.SetActive(true);
        if (!isLoading)
        {
            questSave = new QuestSave(Quest.QuestOrder, true, questName);
            ProgressTracker.Instance.UpdateProgress(questSave);
        }
    }

    public void CheckQuest(bool isLoading)
    {
        if (Quest.QuestCompleted)
        {
            if(questStart)questStart.SetActive(false);
            if(questEnd)questEnd.SetActive(true);
                
            Debug.Log("quest completed");
            CompletedQuest = true;
            AssignedQuest = false;

            if (!isLoading)
            {
                Quest.GiveReward();
                DialogueSystem.Instance.NewDialogue(rewardDialouge, npcName);

                questSave = new QuestSave(Quest.QuestOrder, false, questName);
                ProgressTracker.Instance.UpdateProgress(questSave);
            }
        }
        else
        {
            if(!isLoading)
                DialogueSystem.Instance.NewDialogue(inProgressDialogue,  npcName);
        }
    }
}
