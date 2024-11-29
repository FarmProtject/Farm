using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : InteractableEntity,IInteractable
{



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
        Debug.Log("Range Out!!");
        if (GameManager.instance.playerEntity.nowInteract == this.gameObject && dialoguePanel.activeSelf)
        {
            dialoguePanel.SetActive(false);
            GameManager.instance.playerEntity.nowInteract = null;
            Debug.Log("Range Out!! Script Active!");
        }
    }
    protected virtual void OpenDialogueUI()
    {
        if (!dialoguePanel.activeSelf)
        {
            dialoguePanel.SetActive(true);
        }
        else
        {
            dialoguePanel.SetActive(false);
        }
    }

    private void SetDialogueData()
    {
        // entityName을 이용해 데이터 가져와 세팅
    }


}
