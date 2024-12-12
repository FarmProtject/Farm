using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShopSlotPricePanel : UIBase,IObserver
{
    ShopItemSlot slot;
    TextMeshProUGUI priceText;
    private void Awake()
    {
        OnAwake();
        slot.Attach(this);
    }
    protected override void OnAwake()
    {
        base.OnAwake();
        slot = transform.parent.GetComponent<ShopItemSlot>();
        priceText = transform.GetComponentInChildren<TextMeshProUGUI>();
    }
    protected override void Start()
    {
        
    }
    protected override void OnStart()
    {
        base.OnStart();
        SetPrice();
    }
    void SetPrice()
    {

        if (slot != null && slot.itemData != null)
        {
            ItemBase item = slot.itemData;
            priceText.text = item.price.ToString();
        }
        else
        {
            priceText.text = "Null";
        }
        
    }
    public void Invoke()
    {
        OnStart();
    }
}
