using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
public class ShopPopUpConfirmButton : UIBase,IObserver
{
    [SerializeField]
    Button myButton;
    GameObject popUpPanel;
    protected override void Start()
    {
        
    }
    protected override void OnStart()
    {
        base.OnStart();
    }

    public void Invoke()
    {
        OnStart();
        SetButton();
    }
    void SetButton()
    {
        myButton = transform.GetComponent<Button>();
        myButton.onClick.AddListener(ButtonFunction);
        if (popUpPanel == null)
        {
            popUpPanel = GameObject.Find("ShopPopUpPanel");
        }
    }

    void ButtonFunction()
    {
        if (popUpPanel == null)
        {
            popUpPanel = GameObject.Find("ShopPopUpPanel");
        }
        popUpPanel.SetActive(false);
    }
}
