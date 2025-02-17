using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
public class ToolTipPanel : UIBase
{
    ItemBase item;
    [SerializeField]
    InventorySlot slot;
    [SerializeField]
    Image itemImage;
    [SerializeField]
    SetStringKey headNameText;
    [SerializeField]
    SetStringKey headTypeText;
    [SerializeField]
    SetStringKey itemCountText;
    [SerializeField]
    SetStringKey maxCountText;
    [SerializeField]
    SetStringKey usingEffectText;
    [SerializeField]
    SetStringKey descText;
    [SerializeField]
    TextMeshProUGUI goldValue;
    [SerializeField]
    GameObject bodyPanel;
    [SerializeField]
    GameObject tailPanel;
    private void Awake()
    {
        this.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        UpdateItemInfo();
    }

    void UpdateItem()
    {
        item = slot.GetItem();
    }

    void UpdateItemInfo()
    {
        UpdateItem();
        if (item != null)
        {
            string key = item.id.ToString();
            itemImage.sprite = slot.itemSprite.sprite;
            headNameText.SetItemKey(key);
            descText.SetItemDiscKey(key);
            headTypeText.SetTypeKey(key);
            itemCountText.SetCountKey(key);
            maxCountText.SetMaxCountKey(key);
            usingEffectText.SetEffectKey(key);
            goldValue.text = item.price.ToString();
        }
    }
}
