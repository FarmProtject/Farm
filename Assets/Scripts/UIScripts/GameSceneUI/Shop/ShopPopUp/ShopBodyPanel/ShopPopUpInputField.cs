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
        if (plusButton != null)
        {
            plusButton.onClick.AddListener(PlusItemCount);
        }
        if(minusButton != null)
        {
            minusButton.onClick.AddListener(MinusItemCount);
        }
    }

    void OnEndEdit(string input)
    {
        if(int.TryParse(input,out int result))
        {
            itemCount =  Convert.ToInt32(inputField.text);
            Debug.Log(itemCount);
            if(itemCount < 0)
            {
                itemCount = 0;
                Debug.Log("item Count Less than 0 In ShopPopUpInputField");
            }
            UpdateInputField();
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
            UpdateInputField();
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
                    shopManager.SetInputField();
                }
                break;
            case ShopState.sell:
                if (int.TryParse(input, out result))
                {
                    itemCount = Convert.ToInt32(inputField.text);
                    shopManager.itemCount = itemCount;
                    Debug.Log(shopManager.item.itemCount);
                    shopManager.SetInputField();
                }
                break;
            default:
                break;
        }
    }
    void UpdateInputField() 
    {
        shopManager.item.itemCount = itemCount;
        shopManager.SetInputField();
        inputField.text = shopManager.itemCount.ToString();

    }

    void PlusItemCount()
    {
        Debug.Log(itemCount);
        itemCount++;
        if (itemCount < 0)
        {
            itemCount = 0;
        }
        UpdateInputField();
        if (shopManager.itemCount != itemCount)
        {
            itemCount = shopManager.itemCount;
        }
    }

    void MinusItemCount()
    {
        Debug.Log(itemCount);
        itemCount--;
        if (itemCount < 0)
        {
            itemCount = 0;
        }
        UpdateInputField();
        if (shopManager.itemCount != itemCount)
        {
            itemCount = shopManager.itemCount;
        }
    }


}
