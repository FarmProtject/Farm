using UnityEngine;
using System;
using System.Collections.Generic;
public class BedObject : MonoBehaviour, IInteractable
{
    ConfirmPanel confrimPanel;
    private void Awake()
    {
        
    }
    void OnAwake() 
    {
        UIManager uiManager = GameManager.instance.UIManager;
        confrimPanel = uiManager.confirmPanel;
    }
    public void Interact()
    {
        
    }
}
