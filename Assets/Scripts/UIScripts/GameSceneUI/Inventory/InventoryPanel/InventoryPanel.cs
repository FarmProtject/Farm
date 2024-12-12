using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class InventoryPanel : UIBase,Isubject
{

    List<IObserver> observers = new List<IObserver>();

    private void Awake()
    {
        OnAwake();
    }
    protected override void Start()
    {
        base.Start();
        this.gameObject.SetActive(false);
    }
    protected override void OnStart()
    {
        base.OnStart();
        Notyfy();
    }
    protected override void SetUISize()
    {
        base.SetUISize();
        myRect.pivot = new Vector2(0, 0);
        myRect.sizeDelta = new Vector2(0, 0);
        
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
        foreach(IObserver obser in observers)
        {
            obser.Invoke();
        }
    }
}
