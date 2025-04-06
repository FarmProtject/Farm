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
        if (quickSlotManager.GetSelectSlot() != null && quickSlotManager.GetSelectSlot().item != null)
        {
            quickSlotManager.GetSelectSlot().ItemInvoke();
            Debug.Log("QuickSlot left Click Invoke");
        }
            
        else
        {
            Debug.Log("quickslot data null");
        }
    }
}

public class QuickSlotManager : MonoBehaviour,Isubject
{

    Dictionary<int, ItemBase> quickSlotItems = new Dictionary<int, ItemBase>();
    public QuickSlotLeftClick quickLeftClick ;
    List<QuickSlot> quickSlots = new List<QuickSlot>();
    List<IObserver> observers = new List<IObserver>();
    [SerializeField] GameObject quickSlotPanel;
    [SerializeField] QuickSlot selectSlot;
    int slotNumber;

    private void Awake()
    {
        OnAwake();

    }

    void OnAwake()
    {
        quickLeftClick = new QuickSlotLeftClick(this);
        AddQuickslot();
        QuickSlotSet();
        Notyfy();
    }

    private void Start()
    {
        //SetLeftClick();
    }
    public void SetSelectSlot(QuickSlot slot)
    {
        selectSlot = slot;
    }
    public QuickSlot GetSelectSlot()
    {
        return selectSlot;
    }
    
    void UseQuickSlot()
    {
        if (quickSlotItems[slotNumber] != null)
        {
            quickLeftClick.Invoke();
        }
        
    }
    void AddQuickslot()
    {
        if (quickSlotPanel == null)
        {
            quickSlotPanel = GameObject.Find("QuickSlotPanel").gameObject;
        }

        QuickSlot[] quicks = quickSlotPanel.transform.GetComponentsInChildren<QuickSlot>();
        
        Debug.Log("Added");
        foreach(QuickSlot quick in quicks)
        {
            quickSlots.Add(quick);
        }
        
    }
    void QuickSlotSet()
    {
        Debug.Log("QuickSlot Set");
        Debug.Log($"QuickSlot Count : {quickSlots.Count}");
        KeySettings keySetting = GameManager.instance.keySettings;
        for(int i = 0; i < quickSlots.Count; i++)
        {
            quickSlots[i].slotNumber = i + 1;
            keySetting.quickSlots.Add(quickSlots[i].slotNumber, quickSlots[i]);
            Debug.Log($"quickslot SlotNumber {quickSlots[i].slotNumber}");
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
