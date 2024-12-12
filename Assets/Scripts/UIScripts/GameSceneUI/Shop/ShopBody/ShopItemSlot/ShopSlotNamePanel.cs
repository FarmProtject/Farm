using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShopSlotNamePanel : UIBase,IObserver
{
    ShopItemSlot slot;
    TextMeshProUGUI nameText;
    private void Awake()
    {
        OnAwake();
        slot.Attach(this);
    }
    protected override void OnAwake()
    {
        base.OnAwake();
        slot = transform.parent.GetComponent<ShopItemSlot>();
        nameText = GetComponentInChildren<TextMeshProUGUI>();
    }
    protected override void Start()
    {
        
    }
    protected override void OnStart()
    {
        base.OnStart();
        SetName();
    }
    void SetName()
    {
        if (slot!=null && slot.itemData !=null)
        {
            ItemBase item = slot.itemData;
            nameText.text = item.name;
        }
        else
        {
            nameText.text = "Null";
        }
    }
    public void Invoke()
    {
        OnStart();
    }
}
