using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class InventoryHeadPanel : UIBase, Isubject, IObserver
{
    [SerializeField] InventoryPanel invenPanel;
    List<IObserver> observers = new List<IObserver>();
    private void Awake()
    {
        OnAwake();
    }
    protected override void OnAwake()
    {
        base.OnAwake();
        invenPanel = GameObject.Find("InventoryPanel").transform.GetComponent<InventoryPanel>();
        invenPanel.Attach(this);
    }
    protected override void Start()
    {
        
    }
    protected override void OnStart()
    {
        base.OnStart();
        Notyfy();
    }
    public void Attach(IObserver observer)
    {
        observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void Notyfy()
    {
        foreach(var observer in observers)
        {
            observer.Invoke();
        }
    }

    public void Invoke()
    {
        OnStart();
    }
}
