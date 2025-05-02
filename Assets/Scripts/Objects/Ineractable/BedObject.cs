using UnityEngine;
using System;
using System.Collections.Generic;
public class BedObject : InteractableEntity,IInteractable
{
    [SerializeField]ConfirmPanel confirmPanel;
    DayManager dayManager;
    [SerializeField] string confrimID;
    private void Awake()
    {
        OnAwake();
    }

    protected override void OnAwake()
    {
        uiManager = GameManager.instance.UIManager;

        confirmPanel = uiManager.confirmPanel;
        dayManager = GameManager.instance.dayManager;
        base.OnAwake();
    }

    protected override void NPCInteract()
    {
        
        confirmPanel.yesButton.onClick.AddListener(dayManager.NextDay);
        confirmPanel.yesButton.onClick.AddListener(confirmPanel.DisableConfirm);
        confirmPanel.noButton.onClick.AddListener(confirmPanel.DisableConfirm);
        confirmPanel.descID = confrimID;
        confirmPanel.gameObject.SetActive(true);
    }

    
}
