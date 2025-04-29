using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
public class ConfirmPanel : UIBase
{
    DataManager dm;
    string descID;
    string leftID;
    string rightID;
    TextMeshProUGUI descText;
    TextMeshProUGUI leftText;
    TextMeshProUGUI rightText;
    private void Awake()
    {
        
    }
    void OnAwake()
    {
        dm = GameManager.instance.dataManager;
    }
    void UpdateDescID(string id)
    {
        SetDescID(id);
        //descText.text = GameManager.instance.st
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
