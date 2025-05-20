using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : InteractableEntity,IInteractable
{
    [SerializeField]
    protected int id;
    GameObject player;
    protected override void OnAwake()
    {
        base.OnAwake();
        
        SetDialogueData();
    }

    protected override void NPCInteract()
    {
        base.NPCInteract();
        OpenDialogueUI();
        LookAt();
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
    void LookAt()
    {
        if (player == null)
        {
            player = GameManager.instance.playerEntity.gameObject;
        }
        Vector3 direction = player.transform.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(direction);
        Vector3 rotEuler = rot.eulerAngles;
        rotEuler.x = 0f;
        rotEuler.z = 0f;
        transform.rotation = Quaternion.Euler(rotEuler);
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
