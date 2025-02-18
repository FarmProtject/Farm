using UnityEngine;
using System;
using System.Collections.Generic;
public class ToolTipHeadPanel : UIBase, IObserver
{
    ToolTipPanel tooltipPanel;

    List<UIBase> observers = new List<UIBase>();
    private void Awake()
    {
        OnAwake();
    }
    protected override void Start()
    {

    }
    protected override void OnStart()
    {
        base.OnStart();
        SetObservers();
        Notyfy();
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        tooltipPanel = transform.parent.transform.GetComponent<ToolTipPanel>();
        tooltipPanel.Attach(this);
    }
    public void Invoke()
    {
        OnStart();
    }
    void SetObservers()
    {
        foreach (UIBase child in transform.GetComponentsInChildren<UIBase>())
        {
            if(child.gameObject == this.gameObject)
            {
                return;
            }
            Attach(child);
        }
    }
    public void Attach(UIBase observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }

    public void Detach(UIBase observer)
    {
        if (observers.Contains(observer))
        {
            observers.Remove(observer);
        }
    }
    public void Notyfy()
    {
        foreach (UIBase obs in observers)
        {
            obs.GetStart();
        }
    }
}
