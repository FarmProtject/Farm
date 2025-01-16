using UnityEngine;
using System;
using System.Collections.Generic;
public class ShopPopUpPanel : UIBase, Isubject
{
    List<IObserver> observers = new List<IObserver>();

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
        foreach(IObserver obs in observers)
        {
            obs.Invoke();
        }
    }

    protected override void Start()
    {
        base.Start();
    }
    protected override void OnStart()
    {
        base.OnStart();
        foreach(var obs in this.transform.GetComponentsInChildren<IObserver>())
        {
            observers.Add(obs);
        }
        Notyfy();
    }


}