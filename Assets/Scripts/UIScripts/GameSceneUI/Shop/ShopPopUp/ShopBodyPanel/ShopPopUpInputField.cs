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
        inputField.onValueChanged.AddListener(OnValueChange);
    }

    void OnEndEdit(string input)
    {
        if(int.TryParse(input,out int result))
        {
            itemCount =  Convert.ToInt32(inputField.text);
            Debug.Log(itemCount);
            shopManager.item.itemCount = itemCount;
            shopManager.SetInputField();
            
            Debug.Log("On End Edit!");
        }
        else
        {
            Debug.Log("Input isn't Integer");
        }
    }
    private void OnValueChange(string input)
    {
        if (int.TryParse(input, out int result))
        {
            itemCount = Convert.ToInt32(inputField.text);
            shopManager.item.itemCount = itemCount;
            Debug.Log(shopManager.item.itemCount);
            shopManager.SetInputField();
            Debug.Log("  shop   " + shopManager.item.itemCount);
            Debug.Log("On End Edit!");
        }
    }
    void SetItemData(string input)
    {
        switch (shopManager.shopState)
        {
            case ShopState.buy:
                if (int.TryParse(input, out int result))
                {
                    itemCount = Convert.ToInt32(inputField.text);
                    shopManager.item.itemCount = itemCount;
                    Debug.Log(shopManager.item.itemCount);
                    shopManager.SetInputField();
                    Debug.Log("  shop   " + shopManager.item.itemCount);
                    Debug.Log("On End Edit!");
                }
                break;
            case ShopState.sell:
                if (int.TryParse(input, out result))
                {
                    itemCount = Convert.ToInt32(inputField.text);
                    shopManager.itemCount = itemCount;
                    Debug.Log(shopManager.item.itemCount);
                    shopManager.SetInputField();
                    Debug.Log("  shop   " + shopManager.item.itemCount);
                    Debug.Log("On End Edit!");
                }
                break;
            default:
                break;
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
