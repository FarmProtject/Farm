using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialoguePanel : UIBase
{
    ScriptPanel dialogue;

    private void Awake()
    {
        dialogue = GetComponentInChildren<ScriptPanel>();
    }
    private void OnEnable()
    {
        AddOnUIManager();
        
    }
    private void OnDisable()
    {
        RemoveOnUIManager();
    }
    protected override void Start()
    {
        base.Start();
        SetUISize();
        SetPosition();
        this.gameObject.SetActive(false);
    }
    protected override void SetUISize()
    {
        myRect.anchorMin = new Vector2(0.2f, 0);
        myRect.anchorMax = new Vector2(0.8f, 0.3f);
        myRect.sizeDelta = new Vector2(0, 0);
    }
    protected override void SetPosition()
    {
        myRect.pivot = new Vector2(0.5f, 0f);
        myRect.anchoredPosition = new Vector2(0,0);
    }

    public void SetDialogue(string dialogueText)
    {
        dialogue.SetText(dialogueText);
    }
    public void RemoveDialogue()
    {
        dialogue.SetText("");
    }
}
