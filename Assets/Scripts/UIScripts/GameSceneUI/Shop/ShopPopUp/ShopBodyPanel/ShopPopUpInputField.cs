using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
public class ShopPopUpInputField : UIBase
{
    [SerializeField]
    Button plusButton;
    [SerializeField]
    Button minusButton;
    [SerializeField]
    TMP_InputField inputField;
    ShopManager shopManager;
    PlayerEntity player;
    int itemCount;
    private void Awake()
    {
        inputField = transform.GetComponent<TMP_InputField>();
        shopManager = GameManager.instance.UIManager.shopManager;
        player = GameManager.instance.playerEntity;
        SetInputFiledValue();
    }

    void SetInputFiledValue()
    {
        inputField.contentType = TMP_InputField.ContentType.IntegerNumber;
        inputField.onEndEdit.AddListener(OnEndEdit);
    }

    void OnEndEdit(string input)
    {
        if(int.TryParse(input,out int result))
        {
            itemCount = Int32.Parse(inputField.text);
            shopManager.item.itemCount = itemCount;
            shopManager.SetInputField();
            Debug.Log("On End Edit!");
        }
        else
        {
            Debug.Log("Input isn't Integer");
        }
    }

    void SetMaxCount(int count)
    {
        
    }
    void SetItemCOunt()
    {
        
    }

    void PlusItemCount(int count)
    {

    }

    void MinusItemCount(int count)
    {

    }


}
