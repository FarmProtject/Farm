using UnityEngine;
using System;
using System.Collections.Generic;


public class QuickSlotItemClick : IClickAction
{
    public ItemBase item;
    
    public void SetItem(ItemBase item)
    {
        this.item = item;
    }
    public void Invoke()
    {
        if (item != null && item.itemCount != 0)
        {
        }
        else
        {
        }
    }
}


public class QuickSlotManager : MonoBehaviour
{
    Dictionary<int, ItemBase> quickSlotItems = new Dictionary<int, ItemBase>();
    public QuickSlotItemClick quickUse = new QuickSlotItemClick();
    

    private void Start()
    {
        SetLeftClick();
    }

    void SetLeftClick()
    {
        GameManager.instance.mouseManager.leftClick.Push(quickUse);
    }
    void UseQuickSlot()
    {
        quickUse.Invoke();
    }
}
