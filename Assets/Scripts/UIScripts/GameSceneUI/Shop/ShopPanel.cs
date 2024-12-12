using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : UIBase, Isubject
{
    List<IObserver> observers = new List<IObserver>();

    List<ItemBase> itemList = new List<ItemBase>();

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        itemList.Clear();
    }



    protected override void OnStart()
    {
        base.OnStart();
        Notyfy();
        this.gameObject.SetActive(false);
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
