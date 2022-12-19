using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance { get; set; } 
    [HideInInspector] public List<string> dialogueTexts = new List<string>();
    [HideInInspector] public string npcName;
    
    //Assign from the inspector
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Text npcNameText,dialogueLineText;
    [SerializeField] private Button continueButton;

    private int lineIndex;
    private void Awake()
    {
        continueButton.onClick.AddListener(delegate { ContinueDialogue(); });
        dialoguePanel.SetActive(false);
        /*
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else*/

        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void NewDialogue(string[] texts, string npcName)
    {
        lineIndex = 0;
        dialogueTexts = new List<string>(texts.Length);
        dialogueTexts.AddRange(texts);
        this.npcName = npcName;
        dialoguePanel.SetActive(false);
        MouseCam.EnableCursor(true);
        StartDialogue();
    }

    public void StartDialogue()
    {
        dialogueLineText.text = dialogueTexts[lineIndex];
        npcNameText.text = npcName;
        dialoguePanel.SetActive(true);
    }

    public void ContinueDialogue()
    {
        if (lineIndex < dialogueTexts.Count - 1)
        {
            lineIndex++;
            dialogueLineText.text = dialogueTexts[lineIndex];
        }
        else
        {
            dialoguePanel.SetActive(false);
            MouseCam.EnableCursor(false);
        }

    }
}
