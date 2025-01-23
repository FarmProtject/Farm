using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ShopBodyPanel : UIBase,IObserver,Isubject//,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    ShopPanel shopPanel;
    List<IObserver> observers = new List<IObserver>();
    GameObject contents;
    private void Awake()
    {
        shopPanel = GameObject.Find("ShopPanel").transform.GetComponent<ShopPanel>();
        shopPanel.Attach(this);
        contents = GameObject.Find("ShopContents");
    }


    protected override void Start()
    {
        
    }

    protected override void OnStart()
    {
        base.OnStart();
        Notyfy();
    }

    public void Invoke()
    {
        OnStart();
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
        foreach(var obs in observers)
        {
            obs.Invoke();
        }
    }
    /*
    public void OnBeginDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
    */
}
