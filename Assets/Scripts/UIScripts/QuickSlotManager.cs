using UnityEngine;
using System;
using System.Collections.Generic;

public class QuickSlotLeftClick : IClickAction
{
    QuickSlotManager quickSlotManager;
    public QuickSlotLeftClick(QuickSlotManager quickSlotManager)
    {
        this.quickSlotManager = quickSlotManager;
    }
    public void Invoke()
    {
        if (quickSlotManager.GetSelectSlot() != null && quickSlotManager.GetSelectSlot().item!=null)
            quickSlotManager.GetSelectSlot().ItemInvoke();
    }
}

public class QuickSlotManager : MonoBehaviour,Isubject
{
    Dictionary<int, ItemBase> quickSlotItems = new Dictionary<int, ItemBase>();
    public QuickSlotLeftClick quickLeftClick;
    List<QuickSlot> quickSlots = new List<QuickSlot>();
    List<IObserver> observers = new List<IObserver>();
    QuickSlot selectSlot;
    int slotNumber;

    private void Awake()
    {
        

    }

    void OnAwake()
    {
        quickLeftClick = new QuickSlotLeftClick(this);
        QuickSlotSet();
        Notyfy();
    }

    private void Start()
    {
        SetLeftClick();
    }
    public void SetSelectSlot(QuickSlot slot)
    {
        selectSlot = slot;
    }
    public QuickSlot GetSelectSlot()
    {
        return selectSlot;
    }
    void SetLeftClick()
    {
        GameManager.instance.mouseManager.leftClick.Push(quickLeftClick);
    }
    void UseQuickSlot()
    {
        if (quickSlotItems[slotNumber] != null)
        {
            quickLeftClick.Invoke();
        }
        
    }
    void QuickSlotSet()
    {
        KeySettings keySetting = GameManager.instance.keySettings;
        for(int i = 0; i < quickSlots.Count; i++)
        {
            quickSlots[i].slotNumber = i + 1;
            keySetting.quickSlots.Add(quickSlots[i].slotNumber, quickSlots[i]);
        }
    }
    public void AddInQuickSlot(ItemBase item)
    {

        

    }

    public void Attach(IObserver observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }

    public void Detach(IObserver observer)
    {
        if (observers.Contains(observer))
        {
            observers.Remove(observer);
        }
    }

    public void Notyfy()
    {
        for(int i = 0; i < observers.Count; i++)
        {
            observers[i].Invoke();
        }
    }
}
