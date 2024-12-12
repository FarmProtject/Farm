using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopSlotImagePanel : UIBase,IObserver
{
    ShopItemSlot slot;
    Image itemImage;
    private void Awake()
    {
        OnAwake();
        slot.Attach(this);
    }
    protected override void OnAwake()
    {
        base.OnAwake();
        slot = transform.parent.GetComponent<ShopItemSlot>();
        itemImage = transform.GetChild(0).gameObject.transform.GetComponent<Image>();
    }
    protected override void Start()
    {
        
    }
    protected override void OnStart()
    {
        base.OnStart();
        SetImage();
    }
    void SetImage()
    {
        
        if(slot != null && slot.itemData != null)
        {
            ItemBase item = slot.itemData;
            Debug.Log("Need ItemImageAddFunction In ShopSlotImage Script");
        }
        else
        {

        }
    }
    void SetImageSize()
    {
        RectTransform imageRect = itemImage.GetComponent<RectTransform>();
        imageRect.anchorMin = new Vector2(0.1f, 0.1f);
        imageRect.anchorMax = new Vector2(0.9f, 0.9f);
    }
    public void Invoke()
    {
        OnStart();
    }
}
