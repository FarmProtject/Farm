using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : InteractableEntity,IInteractable
{
    [SerializeField]
    protected int id;
    protected override void OnAwake()
    {
        base.OnAwake();

        SetDialogueData();
    }

    protected override void NPCInteract()
    {
        base.NPCInteract();
        OpenDialogueUI();
    }
    protected override void TriggerOut(Collider other)
    {
        base.TriggerOut(other);
        NPCRangeOut();
    }
    protected virtual void NPCRangeOut()
    {
        uiManager.NPCRangeOut(this);
    }
    protected virtual void OpenDialogueUI()
    {
        uiManager.DialogueUIToggle(this);
    }
    public string GetDialogue()
    {
        return dialogue;
    }
    private void SetDialogueData()
    {
        // entityName을 이용해 데이터 가져와 세팅
        if(dialogue == null)
        {
            dialogue = "Need to Write SetDialogueData Function";
        }
    }


}
