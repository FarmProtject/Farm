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

    public void SetQuickSlot()
    {
        GameManager.instance.keySettings.quickSlots.Add(slotNumber, this);
    }
    public void SetSlotKey()
    {
        KeyCode key = (KeyCode)Enum.Parse(typeof(KeyCode), slotNumber.ToString());
        GameManager.instance.keySettings.quickSlotKeys.Add(key, slotNumber);
    }

    public void Invoke()
    {
        SetQuickSlot();
        SetSlotKey();
    }
}
