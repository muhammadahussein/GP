using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    [SerializeField] private string[] dialogue;
    [SerializeField] protected string[] rewardDialouge;
    [SerializeField] protected string[] inProgressDialogue;
    [SerializeField] protected string[] afterRewardDialouge;
    [SerializeField] protected string npcName;
    public override void Interact()
    {
        DialogueSystem.Instance.NewDialogue(dialogue,npcName);
        Debug.Log("Interact with NPC");
    }
    
}
