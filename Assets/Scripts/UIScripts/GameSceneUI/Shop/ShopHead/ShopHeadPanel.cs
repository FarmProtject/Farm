using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopHeadPanel : UIBase, IObserver,Isubject
{
    ShopPanel shopPanel;
    List<IObserver> observers = new List<IObserver>();
    protected override void OnAwake()
    {
        base.OnAwake();
        shopPanel = transform.parent.GetComponent<ShopPanel>();
        shopPanel.Attach(this);
    }
    protected override void Start()
    {
        
    }

    public void Invoke()
    {
        OnStart();
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
        foreach(var obs in observers)
        {
            obs.Invoke();
        }
    }
}
