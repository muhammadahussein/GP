using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPC : Interactable
{
    [SerializeField] private string[] dialogue;
    [SerializeField] protected string[] rewardDialouge;
    [SerializeField] protected string[] inProgressDialogue;
    [SerializeField] protected string[] afterRewardDialouge;
    [SerializeField] protected string npcName;
    public override void Interact()
    {
        base.Interact();
        DialogueSystem.Instance.NewDialogue(dialogue,npcName);
        Debug.Log("Interact with NPC");
    }
    
}
