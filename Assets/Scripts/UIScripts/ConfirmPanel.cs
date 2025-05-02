using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
public class ConfirmPanel : UIBase
{
    DataManager dm;
    public string descID;
    public string leftID;
    public string rightID;
    [SerializeField]TextMeshProUGUI descText;
    [SerializeField]SetStringKey descString;
    [SerializeField] TextMeshProUGUI leftText;
    [SerializeField] SetStringKey leftString;
    [SerializeField] TextMeshProUGUI rightText;
    [SerializeField] SetStringKey rightString;
    public Button yesButton;
    public Button noButton;
    
    private void Awake()
    {
        
    }

    protected override void OnStart()
    {
        base.OnStart();
        dm = GameManager.instance.dataManager;
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        yesButton.onClick.RemoveAllListeners();
    }
    void UpdateDescID(string id)
    {
        SetDescID(id);
        //descText.text = GameManager.instance.st
    }
    private void OnEnable()
    {
        descString.SetKey(descID);
        leftString.SetKey(leftID);
        rightString.SetKey(rightID);
        descString.EnableFuction();
        leftString.EnableFuction();
        rightString.EnableFuction();
    }
    public void DisableConfirm()
    {
        this.gameObject.SetActive(false);
    }

    void SetDescID(string id)
    {
        descID = id;
    }
    void SetLeftID(string id)
    {
        leftID = id;
    }
    void SetRightID(string id)
    {
        rightID = id;
    }
}
