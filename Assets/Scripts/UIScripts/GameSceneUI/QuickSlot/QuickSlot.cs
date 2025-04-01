using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlot : MonoBehaviour,IObserver
{
    public int slotNumber;
    public ItemBase item;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void Awake()
    {
        
    }
    void OnAwake()
    {
        KeySettings keySet = GameManager.instance.keySettings;
        keySet.quickSlots.Add(slotNumber, this);
    }
    public void SetQuickSlot()
    {
        GameManager.instance.keySettings.quickSlots.Add(slotNumber, this);
    }
    public void SetSlotKey()
    {
        KeyCode key = (KeyCode)Enum.Parse(typeof(KeyCode), slotNumber.ToString());
        GameManager.instance.keySettings.quickSlotKeys.Add(key, slotNumber);
    }
    public void SetItem(ItemBase item)
    {
        this.item = item;
    }
    public void Invoke()
    {
        SetQuickSlot();
        SetSlotKey();
        OnAwake();
    }

    public void ItemInvoke()
    {

    }
}
